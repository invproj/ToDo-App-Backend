using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ToDoAppBackend.Providers
{
    public class RedisProvider
    {
        private readonly ConnectionMultiplexer _connection;

        //TODO: rename to Redis Client and create interface

        public RedisProvider()
        {
            //todo remove this
            _connection = ConnectionMultiplexer.Connect("localhost");
        }

        public void Set<T>(string key, T objectToCache) where T : class
        {
            var db = this._connection.GetDatabase();

            // Todo: emove additional id

            db.StringSet(
                key,
                JsonConvert.SerializeObject(
                    objectToCache,
                    Formatting.Indented,
                    //todo remove
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    }));
        }

        public List<T> ScanAndGet<T>(string pattern) where T : class
        {
            var db = this._connection.GetDatabase();
            var resultList = new List<T>();

            List<string> schemas = new List<string>();

            int nextCursor = 0;
            do
            {
                //todo var everywhere
                var redisResult = db.Execute("SCAN", new object[] { nextCursor.ToString(), "MATCH", pattern });
                //todo tell about this code
                var innerResult = (RedisResult[])redisResult;

                nextCursor = int.Parse((string)innerResult[0]);

                List<string> resultLines = ((string[])innerResult[1]).ToList();
                schemas.AddRange(resultLines);
            }
            while (nextCursor != 0);

            if (schemas.Count <= 0)
            {
                //todo fix
                return new List<T>();
            }

            foreach (var tmpKey in schemas)
            {
                resultList.Add(Get<T>(tmpKey));
            }

            //todo change to default count
            if (resultList.Any())
            {
                return resultList;
            }
            else
            {
                return (List<T>)null;
            }

        }

        public T Get<T>(string key) where T : class
        {
            var db = this._connection.GetDatabase();
            var redisObject = db.StringGet(key);
            if (redisObject.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(redisObject
                        , new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });
            }
            else
            {
                return (T)null;
            }

        }

        public Task Delete(string key)
        {
            var db = this._connection.GetDatabase();
            return db.KeyDeleteAsync(key);
        }

        public void DeleteAll()
        {
            var db = this._connection.GetDatabase();
            db.Execute("FLUSHDB");
            return;
        }

    }
}
