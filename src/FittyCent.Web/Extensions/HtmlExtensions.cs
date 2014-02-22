using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Boxcubic.PropertyMarketing.Web {
    public static class HtmlExtensions {
        private const string FormGroup_ControlCssClasses = "form-control";
        private const string FormGroup_LabelCssClasses = "control-label col-md-3 col-sm-4 col-xs-12";


        public static MvcHtmlString FormGroupDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {
            return FormGroupDisplayFor(html, expression, null);
        }

        public static MvcHtmlString FormGroupDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName) {
            var formGroupTag = CreateTagBuilderWithClass("div", "form-group");
            var label = html.LabelFor(expression, new { @class = FormGroup_LabelCssClasses });
            var hidden = html.HiddenFor(expression);
            var display = html.DisplayFor(expression, templateName);

            var innerDivTag = CreateTagBuilderWithClass("div", "form-control-static", "col-md-6", "col-sm-6", "col-xs-12");

            innerDivTag.InnerHtml = hidden.ToHtmlString() + display.ToHtmlString();
            formGroupTag.InnerHtml = label.ToHtmlString() + innerDivTag.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(formGroupTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FormGroupTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, params string[] cssClasses) {
            return CreateEditorFor(html, expression, html.TextBoxFor, cssClasses);
        }

        public static MvcHtmlString FormGroupTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {
            return CreateEditorFor(html, expression, html.TextAreaFor);
        }

        public static MvcHtmlString FormGroupDropDownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, SelectList selectList) {
            return FormGroupDropDownFor(html, expression, selectList, null);
        }

        public static MvcHtmlString FormGroupDropDownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, SelectList selectList, string optionLabel) {
            return CreateEditorFor(html, expression,
                (exp, htmlAttributes) => html.DropDownListFor(exp, selectList, optionLabel, htmlAttributes));
        }

        public static MvcHtmlString FormGroupPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {
            return CreateEditorFor(html, expression, html.PasswordFor);
        }

        public static MvcHtmlString RowDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {
            return RowDisplayFor(html, expression, null);
        }

        public static MvcHtmlString RowDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName) {
            var rowTag = CreateTagBuilderWithClass("div", "row");

            var label = html.LabelFor(expression, new { @class = "col-md-3 col-sm-4 col-xs-12" });
            var display = html.DisplayFor(expression, templateName);

            var innerDivTag = CreateTagBuilderWithClass("div", "col-md-6", "col-sm-8", "col-xs-9");

            innerDivTag.InnerHtml = display.ToHtmlString();
            rowTag.InnerHtml = label.ToHtmlString() + innerDivTag.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(rowTag.ToString(TagRenderMode.Normal));
        }

        private static MvcHtmlString CreateEditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            Func<Expression<Func<TModel, TValue>>, object, MvcHtmlString> controlFunction, params string[] cssClasses) {
            var controlCssClasses = string.Format("{0} {1}", FormGroup_ControlCssClasses,
                string.Join(" ", cssClasses ?? new string[0]));

            var control = controlFunction(expression, new { @class = controlCssClasses });
            var formGroupTag = CreateTagBuilderWithClass("div", "form-group");
            var label = html.LabelFor(expression, new { @class = FormGroup_LabelCssClasses });

            var validation = html.ValidationMessageFor(expression);

            var innerDivTag = CreateTagBuilderWithClass("div", "col-md-6", "col-sm-8", "col-xs-12");
            innerDivTag.InnerHtml = control.ToHtmlString() + validation.ToHtmlString();
            formGroupTag.InnerHtml = label.ToHtmlString() + innerDivTag.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(formGroupTag.ToString(TagRenderMode.Normal));
        }

        private static TagBuilder CreateTagBuilderWithClass(string name, params string[] cssClasses) {
            var tag = new TagBuilder(name);

            foreach ( var cssClass in cssClasses.Reverse() ) {
                tag.AddCssClass(cssClass);
            }

            return tag;
        }
    }
}