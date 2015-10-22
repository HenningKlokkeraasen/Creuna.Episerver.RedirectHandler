﻿using System;
using System.Collections.Generic;
using System.Linq;
using Creuna.Episerver.RedirectHandler.Core.CustomRedirects;
using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;

namespace Creuna.Episerver.RedirectHandler.Core.Data
{
    [ServiceConfiguration(typeof(DataStoreHandler), Lifecycle = ServiceInstanceScope.Singleton)]
    public class DataStoreHandler
    {
        private readonly IDataStoreFactory _dataStoreFactory;

        public enum GetState
        {
            Saved = 0,
            Suggestion = 1,
            Ignored = 2
        };

        private const string OldUrlPropertyName = "OldUrl";

        public DataStoreHandler(IDataStoreFactory dataStoreFactory)
        {
            _dataStoreFactory = dataStoreFactory;
        }


        public virtual void SaveCustomRedirect(CustomRedirect currentCustomRedirect)
        {
            // Get hold of the datastore
            using (DynamicDataStore store = DataStoreFactory.GetStore(typeof(CustomRedirect)))
            {
                //check if there is an exisiting object with matching property "OldUrl"
                CustomRedirect match =
                    store.Find<CustomRedirect>(OldUrlPropertyName, currentCustomRedirect.OldUrl.ToLower())
=======
            using (var store = GetStore())
            {
                //check if there is an exisiting object with matching property "OldUrl"
                CustomRedirect match =
                    store.Find<CustomRedirect>(OLD_URL_PROPERTY_NAME, currentCustomRedirect.OldUrl.ToLower())
>>>>>>> 0a0248871189094a5e073bb4f5261a97ea2064bc
                        .SingleOrDefault();
                //if there is a match, replace the value.
                if (match != null)
                    store.Save(currentCustomRedirect, match.Id);
                else
                    store.Save(currentCustomRedirect);
            }
        }

        protected virtual DynamicDataStore GetStore()
        {
            return _dataStoreFactory.GetStore(typeof(CustomRedirect));
        }

        /// <summary>
        ///     Returns a list of all CustomRedirect objects in the Dynamic Data Store.
        /// </summary>
        /// <returns></returns>
        public virtual List<CustomRedirect> GetCustomRedirects(bool excludeIgnored)
        {
<<<<<<< HEAD
            using (var store = DataStoreFactory.GetStore(typeof(CustomRedirect)))
            {
                return
                    store.Items<CustomRedirect>()
                        .Where(r => (!excludeIgnored) || r.State.Equals((int)GetState.Saved))
                        .ToList();
=======
            // IEnumerable<CustomRedirect> customRedirects = null;
            using (var store = GetStore())
            {
                IEnumerable<CustomRedirect> customRedirects;
                if (excludeIgnored)
                {
                    customRedirects = from s in store.Items<CustomRedirect>().OrderBy(cr => cr.OldUrl)
                        where s.State.Equals((int) GetState.Saved)
                        select s;
                }
                else
                {
                    customRedirects = from s in store.Items<CustomRedirect>().OrderBy(cr => cr.OldUrl)
                        select s;
                }
                return customRedirects != null ? customRedirects.ToList() : null;
>>>>>>> 0a0248871189094a5e073bb4f5261a97ea2064bc
            }
        }

        public virtual List<CustomRedirect> GetIgnoredRedirect()
        {
<<<<<<< HEAD
            using (var store = DataStoreFactory.GetStore(typeof(CustomRedirect)))
=======
            using (var store = GetStore())
>>>>>>> 0a0248871189094a5e073bb4f5261a97ea2064bc
            {
                IQueryable<CustomRedirect> customRedirects =
                    from s in store.Items<CustomRedirect>().OrderBy(cr => cr.OldUrl)
                    where s.State.Equals(GetState.Ignored)
                    select s;
                return customRedirects.ToList();
            }
        }

        private void UnignoreRedirect()
        {
            // TODO
        }

        /// <summary>
        ///     Delete CustomObject object from Data Store that has given "OldUrl" property
        /// </summary>
        /// <param name="oldUrl"></param>
<<<<<<< HEAD
        public void DeleteCustomRedirect(string oldUrl)
        {
            // Get hold of the datastore
            using (var store = DataStoreFactory.GetStore(typeof(CustomRedirect)))
            {
                //find object with matching property "OldUrl"
                CustomRedirect match =
                    store.Find<CustomRedirect>(OldUrlPropertyName, oldUrl.ToLower()).SingleOrDefault();
=======
        public virtual void DeleteCustomRedirect(string oldUrl)
        {
            // Get hold of the datastore
            using (var store = GetStore())
            {
                //find object with matching property "OldUrl"
                CustomRedirect match =
                    store.Find<CustomRedirect>(OLD_URL_PROPERTY_NAME, oldUrl.ToLower()).SingleOrDefault();
>>>>>>> 0a0248871189094a5e073bb4f5261a97ea2064bc
                if (match != null)
                    store.Delete(match);
            }
        }

        /// <summary>
        ///     Delete all CustomRedirect objects
        /// </summary>
        public virtual void DeleteAllCustomRedirects()
        {
            // In order to avoid a database timeout, we delete the items one by one.
<<<<<<< HEAD
            using (var store = DataStoreFactory.GetStore(typeof(CustomRedirect)))
=======
            using (var store = GetStore())
>>>>>>> 0a0248871189094a5e073bb4f5261a97ea2064bc
            {
                foreach (CustomRedirect redirect in GetCustomRedirects(false))
                {
                    store.Delete(redirect);
                }
            }
        }

        public virtual int DeleteAllIgnoredRedirects()
        {
            // In order to avoid a database timeout, we delete the items one by one.
<<<<<<< HEAD
            using (var store = DataStoreFactory.GetStore(typeof(CustomRedirect)))
=======
            using (var store = GetStore())
>>>>>>> 0a0248871189094a5e073bb4f5261a97ea2064bc
            {
                List<CustomRedirect> ignoredRedirects = GetIgnoredRedirect();
                foreach (CustomRedirect redirect in ignoredRedirects)
                {
                    store.Delete(redirect);
                }
                return ignoredRedirects.Count();
            }
        }


        /// <summary>
        ///     Find all CustomRedirect objects which has a OldUrl og NewUrl that contains the search word.
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns></returns>
        public virtual List<CustomRedirect> SearchCustomRedirects(string searchWord)
        {
<<<<<<< HEAD
            using (var store = DataStoreFactory.GetStore(typeof(CustomRedirect)))
            {
                return (from s in store.Items<CustomRedirect>()
                        where s.NewUrl.Contains(searchWord) || s.OldUrl.Contains(searchWord)
                        select s).ToList();
=======
            using (var store = GetStore())
            {
                IQueryable<CustomRedirect> customRedirects = from s in store.Items<CustomRedirect>()
                    where s.NewUrl.Contains(searchWord) || s.OldUrl.Contains(searchWord)
                    select s;

                return customRedirects != null ? customRedirects.ToList() : null;
>>>>>>> 0a0248871189094a5e073bb4f5261a97ea2064bc
            }
        }
    }
}