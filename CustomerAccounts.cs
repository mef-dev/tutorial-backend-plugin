﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerAccount.Models;
using FluentSiren.Models;
using Microsoft.Extensions.Configuration;
using Natec.Entities;
using Natec.Widecoup;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using UCP.Common.Plugin;
using UCP.Common.Plugin.InternalMef;
using UCP.Common.Plugin.Models.Config;
using UCP.Common.Plugin.Models.MefStatus;
using UCP.Common.Plugin.Services;

namespace CustomerAccount
{
    [Export("customeraccounts", typeof(IBackendPlugin))]
    [ExportMetadata("CustomerAccounts Plugin", "1")]
    [ExportMetadata("ModuleName", "CustomerAccountBackendPlugin")]
    [ExportMetadata("Description", "Адреса")]
    [ExportMetadata("EntityName", "customeraccounts")] 
    public class CustomerAccounts : IBackendPlugin
    {
        private bool UserDeletingApproved = false;
        public IApiContext apiContext = null;

        public (IModifiedTag, BaseEntity) GetById(BaseEntity requestEntity, string lang)
        {
            throw new NotImplementedException();
        }
        public BaseEntity PostEntityAction(string lang, BaseEntity entity, string action, string parent = null)
        {
            throw new NotImplementedException();
        }
        public Entity HateousSchema(BaseEntity baseEntity)
        {
            throw new NotImplementedException();
        }
        public JSchema GetSchema()
        {
            throw new NotImplementedException();
        }
        public PluginConfig GetServiceConfiguration()
        {
            return new PluginConfig()
            {
                UiParameters = new List<PluginConfigSetting> {
                    new PluginConfigSetting {
                        SettingType = PluginConfigSettingType.Text,
                        Name = "JQWidget style name"
                    }
                },
                Logic = new List<PluginConfigSetting> {
                    new PluginConfigSetting{
                        SettingType = PluginConfigSettingType.CheckBox,
                        Name = "Enable user deleting"
                    }
                }
            };
        }
        public void SetServiceConfiguration(PluginConfig config)
        {
            var styleConfig = config.UiParameters.FirstOrDefault(setting => setting.Name == "JQWidget style name");
            if (styleConfig != null) ;//TODO


            var delEnableConfig = config.UiParameters.FirstOrDefault(setting => setting.Name == "JQWidget style name");
            this.UserDeletingApproved = delEnableConfig == null ? false : (bool)delEnableConfig.Value;
        }
        public void SetApiContext(IApiContext context)
        {
            this.apiContext = context;
        }

        public IPagedList<IModifiedTag, BaseEntity> Get(Filter filter)
        {
            filter.PageIndex++;
            IQueryable<CustomerAccounts_Response_GET_OUTPUT> __out;

            using (var db = new Natec.Widecoup.DB(apiContext))
            {
                GET_p_bfd_get_CustomerAccounts_Request request = new GET_p_bfd_get_CustomerAccounts_Request()
                {
                    PageNumber = filter.PageIndex,
                    PageSize = filter.PageSize,
                };
               
                var testResult = db.Procedures.CallRequestResponseQuery<Natec.Widecoup.GET_p_bfd_get_CustomerAccounts_Request, Natec.Widecoup.GET_p_bfd_get_CustomerAccounts_Response>(request);

                var rawData = testResult.ResultSet.Select(d => Natec.Widecoup.MappingService.Map<Natec.Widecoup.GET_p_bfd_get_CustomerAccounts_Response_OUTPUT>(d));
                var webData = rawData.Select(r => Natec.Widecoup.MappingService.Map<CustomerAccounts_Response_GET_OUTPUT>(r).Clean()).ToList();

                __out = webData.Select(x => (CustomerAccounts_Response_GET_OUTPUT) x).ToArray().AsQueryable();

            }
            var rez = new PagedList<IModifiedTag, CustomerAccounts_Response_GET_OUTPUT>(__out, filter.PageIndex, filter.PageSize);
            rez.Data = __out;
            return rez;

        }
        public BaseEntity Post(string lang, BaseEntity entity, string parent = null)
        {

            var plugin = new Natec.Entities.CustomersPlugin();
            plugin.SetApiContext(apiContext);
            
            var requestData = new CustomersRequest()
            {
                IDType = 0,
                ProfileType = 1,
                Name = entity.Name,
                ParentNode = 1,
                StatusID = 5,
                ParentID = 1,
                isUpdateNodeTree = 1,
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(requestData, new ValidationContext(requestData), results, true);

            if (!isValid)
                throw new ArgumentException("request model is not valid");

            BaseEntity rez;
            
            rez = plugin.Post("uk", requestData);
            
            return rez;
        }
        public BaseEntity Put(string id, string lang, BaseEntity entity)
        {
            var plugin = new Natec.Entities.CustomersPlugin();
            plugin.SetApiContext(apiContext);

            Natec.Widecoup.Customers_Response_GETBYID_OUTPUT pluginCustomerToEdit;

            pluginCustomerToEdit = (Customers_Response_GETBYID_OUTPUT) plugin.GetById(new Customers_Request_GETBYID
            {
                abn_ID = Int32.Parse(entity.Id),
                lang = "uk"
            }, "uk").Item2;

            pluginCustomerToEdit.AbonentFullName = entity.Name;

            var requestData = new CustomersRequest()
            {
                IDType = 0,
                ProfileType = pluginCustomerToEdit.ProfileType,
                Name = pluginCustomerToEdit.AbonentFullName,
                isUpdateNodeTree = 1,
                ID = pluginCustomerToEdit.abn_ExternalID
            };

            var validErr = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(requestData, new ValidationContext(requestData), validErr, true);

            if (!isValid)
                throw new ArgumentException("request model is not valid");
            var rez = plugin.Put(pluginCustomerToEdit.abn_ID.ToString(), "uk", requestData);
            return rez;
        }
        public void Delete(string Id)
        {
            if (!this.UserDeletingApproved)
                return;
            var plugin = new Natec.Entities.CustomersPlugin();
            plugin.SetApiContext(apiContext);
            plugin.Delete(Id);

        }
    }
}