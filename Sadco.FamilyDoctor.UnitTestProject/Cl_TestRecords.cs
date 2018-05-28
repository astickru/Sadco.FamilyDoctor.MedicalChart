using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;

namespace Sadco.FamilyDoctor.UnitTestProject
{
    [TestClass]
    public class Cl_TestRecords
    {
        [TestMethod]
        public void f_TestElementVisibleFromFormula()
        {
            //tag_one = "pat2"; tag_dva = 11; tag_tri = 5
            var record = new Cl_Record() { p_Title = "Тест проверки формул" };
            record.p_Values = new List<Cl_RecordValue>();

            var template = new Cl_Template() { p_Name = "Тест проверки формул", p_Type = Cl_Template.E_TemplateType.Template };
            var elements = new List<Cl_TemplateElement>();
            var element = new Cl_Element()
            {
                p_ID = 1,
                p_Name = "Формула 1",
                p_Tag = "one"
            };
            element.p_ParamsValues = new List<Cl_ElementParam>();
            for (int i = 1; i < 4; i++)
            {
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.NormValues, p_Value = "norm" + i.ToString() });
                var ep = new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.PatValues, p_Value = "pat" + i.ToString() };
                if (i == 2)
                {
                    record.p_Values.Add(new Cl_RecordValue()
                    {
                        p_ElementID = element.p_ID,
                        p_Element = element,
                        p_Params = new List<Cl_RecordParam>() { new Cl_RecordParam() { p_ElementParam = ep } }
                    });
                }
                element.p_ParamsValues.Add(ep);
            }
            elements.Add(new Cl_TemplateElement() { p_Template = template, p_ChildElement = element, p_Index = 0 });
            element = new Cl_Element()
            {
                p_ID = 2,
                p_Name = "Формула 2",
                p_Tag = "dva",
                p_IsNumber = true,
                p_NumberRound = 2
            };
            element.p_ParamsValues = new List<Cl_ElementParam>();
            for (int i = 1; i < 4; i++)
            {
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.NormValues, p_Value = i.ToString() });
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.PatValues, p_Value = i.ToString() });
            }
            record.p_Values.Add(new Cl_RecordValue() { p_ElementID = element.p_ID, p_Element = element, p_ValueUser = "11" });
            elements.Add(new Cl_TemplateElement() { p_Template = template, p_ChildElement = element, p_Index = 1 });
            element = new Cl_Element()
            {
                p_ID = 3,
                p_Name = "Формула 3",
                p_Tag = "tri",
                p_IsNumber = true,
                p_NumberRound = 2
            };
            element.p_ParamsValues = new List<Cl_ElementParam>();
            for (int i = 1; i < 4; i++)
            {
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.NormValues, p_Value = i.ToString() });
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.PatValues, p_Value = i.ToString() });
            }
            record.p_Values.Add(new Cl_RecordValue() { p_ElementID = element.p_ID, p_Element = element, p_ValueUser = "5" });
            elements.Add(new Cl_TemplateElement() { p_Template = template, p_ChildElement = element, p_Index = 2 });
            template.p_TemplateElements = elements;
            record.f_SetTemplate(template);

            bool actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\"");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = tag_one");
            Assert.AreEqual(true, actual);

            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_dva > 10");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_dva < 15");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_dva = 11");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_dva > 14");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_dva < 11");
            Assert.AreEqual(false, actual);

            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva > 10 ИЛИ tag_tri = 3");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva < 12 ИЛИ tag_tri = 3");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 11 ИЛИ tag_tri = 3");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 13 ИЛИ tag_tri = 5");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 13 ИЛИ tag_tri > 4");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 13 ИЛИ tag_tri < 6");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 13 ИЛИ tag_tri < tag_dva");
            Assert.AreEqual(true, actual);

            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 13 ИЛИ tag_tri > tag_dva");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 13 ИЛИ tag_tri = 6");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva = 13");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva > 11");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat2\" И tag_dva < 11");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"pat3\" И tag_dva = 11");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_one = \"\" И tag_dva = 11");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void f_TestElementMathematicalFormula()
        {
            //tag_one = "14"; tag_dva = 11; tag_tri = 5
            var record = new Cl_Record() { p_Title = "Тест проверки формул" };
            record.p_Values = new List<Cl_RecordValue>();

            var template = new Cl_Template() { p_Name = "Тест проверки формул", p_Type = Cl_Template.E_TemplateType.Template };
            var elements = new List<Cl_TemplateElement>();

            var element = new Cl_Element()
            {
                p_ID = 1,
                p_Name = "Формула 1",
                p_Tag = "one",
                p_IsNumber = true,
                p_NumberRound = 3
            };
            element.p_ParamsValues = new List<Cl_ElementParam>();
            for (int i = 1; i < 4; i++)
            {
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.NormValues, p_Value = i.ToString() });
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.PatValues, p_Value = i.ToString() });
            }
            record.p_Values.Add(new Cl_RecordValue() { p_ElementID = element.p_ID, p_Element = element, p_ValueUser = "14" });
            elements.Add(new Cl_TemplateElement() { p_Template = template, p_ChildElement = element, p_Index = 0 });
            element = new Cl_Element()
            {
                p_ID = 2,
                p_Name = "Формула 2",
                p_Tag = "dva",
                p_IsNumber = true,
                p_NumberRound = 2
            };
            element.p_ParamsValues = new List<Cl_ElementParam>();
            for (int i = 1; i < 4; i++)
            {
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.NormValues, p_Value = i.ToString() });
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.PatValues, p_Value = i.ToString() });
            }
            record.p_Values.Add(new Cl_RecordValue() { p_ElementID = element.p_ID, p_Element = element, p_ValueUser = "11" });
            elements.Add(new Cl_TemplateElement() { p_Template = template, p_ChildElement = element, p_Index = 1 });
            element = new Cl_Element()
            {
                p_ID = 3,
                p_Name = "Формула 3",
                p_Tag = "tri",
                p_IsNumber = true,
                p_NumberRound = 2
            };
            element.p_ParamsValues = new List<Cl_ElementParam>();
            for (int i = 1; i < 4; i++)
            {
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.NormValues, p_Value = i.ToString() });
                element.p_ParamsValues.Add(new Cl_ElementParam() { p_Element = element, p_TypeParam = Cl_ElementParam.E_TypeParam.PatValues, p_Value = i.ToString() });
            }
            record.p_Values.Add(new Cl_RecordValue() { p_ElementID = element.p_ID, p_Element = element, p_ValueUser = "5" });
            elements.Add(new Cl_TemplateElement() { p_Template = template, p_ChildElement = element, p_Index = 2 });
            template.p_TemplateElements = elements;
            record.f_SetTemplate(template);

            decimal? result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_one + 3");
            Assert.AreEqual(17, result);
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_one + 8");
            Assert.AreEqual(22, result);

            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_one - 3");
            Assert.AreEqual(11, result);
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_dva - 8");
            Assert.AreEqual(3, result);
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_dva * 11");
            Assert.AreEqual(121, result);
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_dva / 2");
            Assert.AreEqual((decimal)5.5, result);
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_tri - 7");
            Assert.AreEqual(-2, result);

            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_one + tag_dva - tag_tri");
            Assert.AreEqual(20, result);
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, "tag_one * tag_dva / tag_tri");
            Assert.AreEqual((decimal)30.8, result);
        }
    }
}
