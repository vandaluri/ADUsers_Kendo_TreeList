using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace ADUser_TreeList.Models
{
    public class GetADUsers
    {

        public IEnumerable<EmployeeDTO> GetListOfAdUsersByGroup()
        {
            var empDTO = new List<EmployeeDTO>();
            EmployeeDTO emp;
            var entry = new DirectoryEntry("LDAP://fepwdcv01/OU=Users,OU=DCF,DC=DCFINT,DC=WISTATE,DC=US");
            var search = new DirectorySearcher(entry);
            var de = new DirectoryEntry { Path = "LDAP://OU=Users,OU=DCF,DC=DCFINT,DC=WISTATE,DC=US" };
            search.Filter = "(&(objectCategory=person)(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2)) )";
            search.PropertiesToLoad.Add("name");
            search.PropertiesToLoad.Add("employeeID");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("cn");
            search.PropertiesToLoad.Add("Manager");
            search.PropertiesToLoad.Add("wiBureau");
            search.SearchRoot = de;
            search.PageSize = 4000;
            var mySearchResultColl = search.FindAll();
            foreach (SearchResult result in mySearchResultColl)
            {
                var name = result.Properties["name"].Count > 0
                    ? result.Properties["name"][0].ToString().ToUpper()
                    : string.Empty;
                var isValidName = (name.StartsWith("A_") || name.Contains("TEST") || name.Contains("TRAIN") ||
                                   name.Contains("BUILD") ||
                                   name.Contains("CONFIG") || name.Contains("TRN") || name.Contains("GEF") ||
                                   name.Contains("BMCW") ||
                                   name.Contains("DESK"));
                if (isValidName) continue;
                emp = new EmployeeDTO
                {
                    Id = Convert.ToInt32(result.Properties["employeeID"][0]),

                    TeamMember = result.Properties["name"].Count > 0
                        ? result.Properties["name"][0].ToString()
                        : string.Empty,
                    Email = result.Properties["mail"].Count > 0
                        ? result.Properties["mail"][0].ToString()
                        : string.Empty,
                    Manager = result.Properties["Manager"].Count > 0
                        ? result.Properties["Manager"][0].ToString()
                        : string.Empty,
                    Buerau = result.Properties["wiBureau"].Count > 0
                        ? result.Properties["wiBureau"][0].ToString()
                        : string.Empty,
                    cn = result.Properties["cn"].Count > 0
                        ? result.Properties["cn"][0].ToString()
                        : string.Empty,

                };
                empDTO.Add(emp);

            }
            
            return GetManagerIds(empDTO);
        }

        public IList<EmployeeDTO> GetManagerIds(IEnumerable<EmployeeDTO> empDTO)
        {
            //var empMgrDTO = new List<EmployeeDTO>();
            var MgrData = from r in empDTO
                          join l in empDTO on r.cn equals l.ManagerCn
                          select new EmployeeDTO
                          {
                              Id = l.Id,
                              cn = l.cn,
                              ManagerCn = l.ManagerCn,
                              ManagerId = r.Id
                          };

            var Modeldata = MgrData.ToList().Select(emp => new EmployeeDTO
            {
                Id = emp.Id,
                ManagerId = emp.ManagerId == emp.Id ? null : emp.ManagerId,
                Buerau = emp.Buerau,
                Email = emp.Email,
                cn = emp.cn,
                Manager = emp.Manager,
                ManagerCn = emp.ManagerCn,
                TeamMember = emp.TeamMember
            });


            return Modeldata.ToList();
        }


    }
}