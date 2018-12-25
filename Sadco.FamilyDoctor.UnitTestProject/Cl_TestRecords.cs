using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;

namespace Sadco.FamilyDoctor.UnitTestProject
{
    [TestClass]
    public class Cl_TestRecords
    {
        private string f_GetLocalResourcesPath()
        {
            return "c:/temp/MedicalChart/Resources";
        }

        private string f_GetConnectionString()
        {
            return @"Server=(localdb)\mssqllocaldb;Database=MedicalChart;Trusted_Connection=True;ConnectRetryCount=0";
        }

        [TestMethod]
        public void f_TestElementVisibleFromFormula()
        {
            //tag_one = "pat2"; tag_dva = 11; tag_tri = 5
            var record = new Cl_Record() { p_Title = "Тест проверки формул" };
            record.p_DateCreate = DateTime.Now;
            var medicalCard = new Cl_MedicalCard();
            medicalCard.p_PatientSex = Core.Permision.Cl_User.E_Sex.Man;
            medicalCard.p_PatientDateBirth = new DateTime(1981, 4, 1);
            record.p_MedicalCard = medicalCard;
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

            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_gender = man И tag_age > 10");
            Assert.AreEqual(true, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_gender = man И tag_age > 40");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_gender = female И tag_age > 10");
            Assert.AreEqual(false, actual);
            actual = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, "tag_gender = female");
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

            element = new Cl_Element()
            {
                p_ID = 4,
                p_Name = "Формула 4",
                p_Tag = "chet",
                p_IsNumber = true,
                p_NumberRound = 2,
                p_NumberFormula = "tag_dva - 100"
            };
            //record.p_Values.Add(new Cl_RecordValue() { p_ElementID = element.p_ID, p_Element = element });
            elements.Add(new Cl_TemplateElement() { p_Template = template, p_ChildElement = element, p_Index = 4 });

            template.p_TemplateElements = elements;
            record.f_SetTemplate(template);

            element.p_NumberFormula = "tag_one + 3";
            decimal? result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual(17, result);
            element.p_NumberFormula = "tag_one + 8";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual(22, result);

            element.p_NumberFormula = "tag_one - 3";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual(11, result);
            element.p_NumberFormula = "tag_dva - 8";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual(3, result);
            element.p_NumberFormula = "tag_dva * 11";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual(121, result);
            element.p_NumberFormula = "tag_dva / 2";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual((decimal)5.5, result);
            element.p_NumberFormula = "tag_tri - 7";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual(-2, result);
            element.p_NumberFormula = "tag_one + tag_dva - tag_tri";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual(20, result);
            element.p_NumberFormula = "tag_one * tag_dva / tag_tri";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual((decimal)30.8, result);
            element.p_NumberFormula = "tag_one / tag_chet / tag_chet";
            result = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, element);
            Assert.AreEqual((decimal)0.001767, Math.Round(result.Value, 6));
        }

        [TestMethod]
        public void f_TestCreateRecord()
        {
            var temlName = "Тест проверки создания API работы с записями";
            var dc = new Cl_DataContextMegaTemplate(f_GetConnectionString());
            dc.f_Init();
            var result = Cl_MedicalCardsFacade.f_GetInstance().f_Init(dc);
            Assert.AreEqual(true, result);
            result = Cl_TemplatesFacade.f_GetInstance().f_Init(dc);
            Assert.AreEqual(true, result);
            result = Cl_RecordsFacade.f_GetInstance().f_Init(dc, f_GetLocalResourcesPath());
            Assert.AreEqual(true, result);
            result = Cl_CatalogsFacade.f_GetInstance().f_Init(dc);
            Assert.AreEqual(true, result);

            var groupTpl = dc.p_Groups.FirstOrDefault(g => g.p_Type == Cl_Group.E_Type.Templates && g.p_Name == "test");
            if (groupTpl == null)
            {
                groupTpl = new Cl_Group() { p_Name = "test", p_Type = Cl_Group.E_Type.Elements };
                dc.p_Groups.Add(groupTpl);
                dc.SaveChanges();
            }
            var groupEl = dc.p_Groups.FirstOrDefault(g => g.p_Type == Cl_Group.E_Type.Templates && g.p_Name == "test");
            if (groupEl == null)
            {
                groupEl = new Cl_Group() { p_Name = "test", p_Type = Cl_Group.E_Type.Elements };
                dc.p_Groups.Add(groupEl);
                dc.SaveChanges();
            }

            if (!Cl_CatalogsFacade.f_GetInstance().f_HasCategory("Осмотр"))
                Cl_CatalogsFacade.f_GetInstance().f_AddCategory(Cl_Category.E_CategoriesTypes.Total, "Осмотр");
            if (!Cl_CatalogsFacade.f_GetInstance().f_HasCategory("Клиническая 1"))
                Cl_CatalogsFacade.f_GetInstance().f_AddCategory(Cl_Category.E_CategoriesTypes.Clinic, "Клиническая 1");

            var catTotal = Cl_CatalogsFacade.f_GetInstance().f_GetCategory("Осмотр");
            Assert.AreNotEqual(null, catTotal);
            var catClinic = Cl_CatalogsFacade.f_GetInstance().f_GetCategory("Клиническая 1");
            Assert.AreNotEqual(null, catClinic);

            var tmpl = Cl_TemplatesFacade.f_GetInstance().f_GetTemplateByName(temlName);
            if (tmpl == null)
            {
                tmpl = new Cl_Template() { p_Name = temlName, p_Type = Cl_Template.E_TemplateType.Template };
                var elements = new List<Cl_TemplateElement>();

                var element = new Cl_Element()
                {
                    p_ParentGroupID = groupEl.p_ID,
                    p_ParentGroup = groupEl,
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
                elements.Add(new Cl_TemplateElement() { p_Template = tmpl, p_ChildElement = element, p_Index = 0 });

                element = new Cl_Element()
                {
                    p_ParentGroupID = groupEl.p_ID,
                    p_ParentGroup = groupEl,
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
                elements.Add(new Cl_TemplateElement() { p_Template = tmpl, p_ChildElement = element, p_Index = 1 });
                element = new Cl_Element()
                {
                    p_ParentGroupID = groupEl.p_ID,
                    p_ParentGroup = groupEl,
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
                elements.Add(new Cl_TemplateElement() { p_Template = tmpl, p_ChildElement = element, p_Index = 2 });
                tmpl.p_ParentGroupID = groupTpl.p_ID;
                tmpl.p_ParentGroup = groupTpl;
                tmpl.p_TemplateElements = elements;
                tmpl.p_CategoryTotalID = catTotal.p_ID;
                tmpl.p_CategoryTotal = catTotal;
                tmpl.p_CategoryClinicID = catClinic.p_ID;
                tmpl.p_CategoryClinic = catClinic;

                dc.p_Templates.Add(tmpl);
                dc.SaveChanges();
            }
            Assert.AreNotEqual(null, tmpl);

            var elts = Cl_TemplatesFacade.f_GetInstance().f_GetElements(tmpl);
            Assert.AreNotEqual(null, elts);
            var vals = new List<Cl_RecordValue>();
            foreach (var el in elts)
            {
                vals.Add(new Cl_RecordValue() { p_ElementID = el.p_ID, p_Element = el, p_ValueUser = "5" });
            }
            var medicalCard1 = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard("777", 1);
            if (medicalCard1 == null)
                medicalCard1 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("777", 1, Core.Permision.Cl_User.E_Sex.Man, "Иванов", "Иван", "Иванович", new DateTime(1996, 3, 11), "Медкарта API тест 777");
            Assert.IsNotNull(medicalCard1);
            result = Cl_RecordsFacade.f_GetInstance().f_CreateRecord(medicalCard1, catTotal, catClinic, "Заголовок API тест - значения", "Клиника API тест значения", 56369,
                "Доктор_Фамилия", "Доктор_Имя", "Доктор_Отчество",
                 tmpl, vals);
            Assert.AreEqual(true, result);
            result = Cl_RecordsFacade.f_GetInstance().f_CreateRecord(medicalCard1, catTotal, catClinic, "Заголовок API тест - файл", "Клиника API тест файл", 56369,
                "Доктор_Фамилия", "Доктор_Имя", "Доктор_Отчество",
                E_RecordFileType.HTML, Encoding.UTF8.GetBytes("<h1>API тест файл<h1>"));
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void f_TestCreateMedicalCard()
        {
            var dc = new Cl_DataContextMegaTemplate(f_GetConnectionString());
            dc.f_Init();
            var result = Cl_MedicalCardsFacade.f_GetInstance().f_Init(dc);
            Assert.AreEqual(true, result);

            using (var transaction = dc.Database.BeginTransaction())
            {
                try
                {
                    var medicalCard1 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("001", 1, Core.Permision.Cl_User.E_Sex.Man, "Иванов", "Иван", "Иванович", new DateTime(1996, 3, 11), "Медкарта API тест 001");
                    Assert.IsNotNull(medicalCard1);
                    var medicalCard2 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("002", 2, Core.Permision.Cl_User.E_Sex.Man, "Петров", "Петр", "Петрович", new DateTime(1997, 3, 11), "Медкарта API тест 002");
                    Assert.IsNotNull(medicalCard2);
                    var medicalCard3 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("003", 3, Core.Permision.Cl_User.E_Sex.Female, "Александрова", "Александра", "Александровна", new DateTime(1998, 3, 11), "Медкарта API тест 003");
                    Assert.IsNotNull(medicalCard2);
                    var medicalCard4 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("004", 4, Core.Permision.Cl_User.E_Sex.Female, "Сидорова", "Александра", "Александровна", new DateTime(1999, 3, 11), "Медкарта API тест 004");
                    Assert.IsNotNull(medicalCard4);
                    var medicalCard5 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("005", 5, Core.Permision.Cl_User.E_Sex.Man, "Петров", "Александра", "Петрович", new DateTime(2000, 3, 11), "Медкарта API тест 005");
                    Assert.IsNotNull(medicalCard5);
                    var medicalCard6 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("006", 5, Core.Permision.Cl_User.E_Sex.Man, "Иванов", "Михаил", "Иванович", new DateTime(2001, 3, 11), "Медкарта API тест 006");
                    Assert.IsNotNull(medicalCard6);
                    var medicalCard7 = Cl_MedicalCardsFacade.f_GetInstance().f_CreateMedicalCard("007", 5, Core.Permision.Cl_User.E_Sex.Man, "Сидоров", "Петр", "Александрович", new DateTime(2002, 3, 11), "Медкарта API тест 007");
                    Assert.IsNotNull(medicalCard7);

                    var sdfd = dc.p_MedicalCards.ToList();


                    result = Cl_MedicalCardsFacade.f_GetInstance().f_DeleteMedicalCard("001", 2);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_DeleteMedicalCard("002", 1);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_DeleteMedicalCard("001", 1);
                    Assert.AreEqual(true, result);

                    result = Cl_MedicalCardsFacade.f_GetInstance().f_ArchiveMedicalCard("001", 2);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_ArchiveMedicalCard("002", 1);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_ArchiveMedicalCard("002", 2);
                    Assert.AreEqual(true, result);

                    result = Cl_MedicalCardsFacade.f_GetInstance().f_MergeMedicalCards("003", 4, "004", 4);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_MergeMedicalCards("004", 3, "004", 4);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_MergeMedicalCards("003", 3, "003", 4);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_MergeMedicalCards("003", 3, "004", 3);
                    Assert.AreEqual(false, result);

                    result = Cl_MedicalCardsFacade.f_GetInstance().f_MergeMedicalCards("003", 3, "004", 4);
                    Assert.AreEqual(true, result);
                    var medSource = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard("003", 3);
                    Assert.IsNotNull(medSource);
                    Assert.AreEqual(true, medSource.p_IsDelete);
                    var medTarget = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard("004", 4);
                    Assert.IsNotNull(medTarget);

                    result = Cl_MedicalCardsFacade.f_GetInstance().f_MergeMedicalCards(100);
                    Assert.AreEqual(false, result);
                    result = Cl_MedicalCardsFacade.f_GetInstance().f_MergeMedicalCards(5);
                    Assert.AreEqual(true, result);
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    throw er;
                }
            }
        }
    }
}
