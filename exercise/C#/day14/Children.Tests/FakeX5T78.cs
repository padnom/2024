using Children.Db2;
using Children.DTOs;

namespace Children.Tests;

public static class FakeX5T78
{
    public static X5T78 CreateGirl()
    {
        return   new X5T78
                 {
                                   Id = Guid.NewGuid().ToString(),
                                   N_1 = "Alice",
                                   N_2 = "Marie",
                                   N_3 = "Smith",
                                   CityOfBirth__pc = "Paradise",
                                   Person_BD = "19/03/2017",
                                   Salutation = "Girl",
                                   Type_pc = "PPMPX_09/1",
                                   Serv__Gender__TYPE_pc = "X",
                                   DeclaredMonthlySalary__c = "0",
                                   LegalDocumentExpirationDate1__c = "01/09/2030",
                                   LegalDocumentIssuingCountry1__c = "Paradise",
                                   LegalDocumentName1__c = "ID",
                                   LegalDocumentNumber1__c = "9892389098",
                                   ST_Num = "123",
                                   ST____Name = "Sunny Street",
                                   ST_C = "Paradise",
                                   ST_CID = "99"
                 };
    }

    public static X5T78 CreateBoy()
    {
        return new X5T78(id: Guid.NewGuid()
                          .ToString(),
                  n1: "Bob",
                  n3: "Brown",
                  cityOfBirthPc: "Paradise",
                  personBd: "01/09/2021",
                  salutation: "Boy",
                  typePc: "PP0PLX_09/1",
                  servGenderTypePc: "VJX",
                  declaredMonthlySalaryC: "0",
                  legalDocumentExpirationDate1C: "12/09/2078",
                  legalDocumentIssuingCountry1C: "Paradise",
                  legalDocumentName1C: "ID",
                  legalDocumentNumber1C: "9U129731873191JK",
                  stNum: "9",
                  stName: "Oak Street",
                  stC: "Paradise",
                  stCid: "98988");
    }
}