using System.Collections.Generic;
using System;

using riminder;

namespace riminder.UnitTests
{
    public class TestHelper
    {
        private static TestHelper instance = null;
        private static readonly object mutx = new object();

        string _source_id;
        string _profile_id;
        string _profile_reference;
        string _filter_id;
        string _filter_reference;

        string _webhook_key;
        string _secret_key;
        string _source_name;
        Riminder _client;

        TestHelper()
        {
            _webhook_key = "som reallllllllly good key";
            _secret_key = "";
            _source_name = "sdk_test";    
            _client = new Riminder(_secret_key, _webhook_key);
        }

        public static TestHelper Instance
        {
            get
            {
                lock(mutx)
                {
                    if (instance == null)
                        instance = new TestHelper();
                    return instance;
                }
            }
        }

        public Riminder Client
        {
            get
            {
                return _client;
            }
        }

        public string Source_id
        {
            get
            {
                if (_source_id != null)
                    return _source_id;
                var sources = _client.source.list();
                if (_source_name != null)
                {
                    var source_tmp = sources.Find(x => _source_name == x.name);
                    if (source_tmp.source_id != null)
                        _source_id = source_tmp.source_id;
                    else
                        throw new riminder.exp.RiminderArgumentException(String.Format("Source {0} not found", _source_name));
                }
                _source_id = sources[0].source_id;
                return _source_id;
            }
        }

        private void getFilter()
        {
            var filters = _client.filter.list();
            var filter = filters.Find(x => x.filter_id != null && x.filter_reference != null);
            if (filter.filter_id == null)
                throw new riminder.exp.RiminderArgumentException(String.Format("No valid filter found."));
            
            _filter_id = filter.filter_id;
            _filter_reference = filter.filter_reference;
        }

        public string Filter_id
        {
            get
            {
                if (_filter_id != null)
                    return _filter_id;
                getFilter();
                return _filter_id;                
            }
        }

        public string Filter_reference
        {
            get
            {
                if (_filter_reference != null)
                    return _filter_reference;
                getFilter();
                return _filter_reference;    
            }
        }

        private void getProfile()
        {
            List<string> source_ids = new List<string> {this.Source_id};
            var profiles = _client.profile.list(source_ids);
            var profile = profiles.profiles.Find(x => x.profile_id != null && x.profile_reference != null);
            if (profile.profile_id == null)
                 throw new riminder.exp.RiminderArgumentException(String.Format("No valid profile found."));
            _profile_id = profile.profile_id;
            _profile_reference = profile.profile_reference;
        }

        public string Profile_id
        {
            get
            {
                if (_profile_id != null)
                    return _profile_id;
                getProfile();
                return _profile_id; 
            }
        }

        public string Profile_reference
        {
            get
            {
                if (_profile_reference != null)
                    return _profile_reference;
                getProfile();
                return _profile_reference; 
            }
        }

        public static bool check_date(riminder.response.Date date)
        {
            if (date.date == null && date.timezone == null)
            {
                return true;
            }
            return date.date != null && date.timezone != null;
        }

    }
}