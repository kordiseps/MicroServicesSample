# MicroServicesSample



Docker işlemleri

docker run -d --hostname microservice-rabbitmq --name rabbit-server -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=admin -p 15672:15672 -p 5672:5672 rabbitmq:3-management
http://localhost:15672 adresinden managemen dashboarda girilebilir.
http://localhost:15672/#/users adresinden  virtual host oluşturulabilir. Uygulama için FileVirtualHost ve MailVirtualHost oluşturdum
confighelper'a
{
  'Host': 'localhost',
  'Port': 5672,
  'User': 'admin',
  'PassWord': 'admin'
}
şeklinde eklendi


docker run -d -e "ACCEPT_EULA=Y" --name mssql-server -e "SA_PASSWORD=secretP4ssword" -p 14334:1433 -d mcr.microsoft.com/mssql/server:2017-latest
// -p 1433:1433 yazarsak bilgisayarda kurulu olanla çakışabileceği için  -p 14334:1433 yazarak başka porttan iletişime geçiyoruz
hostname: localhost,14334 //
user: sa
password: secretP4ssword
ile bağlantı kurulabilir. 
şifre en az 8 karakter büyük küçük harf ve rakam içermezse mssql docker container hata verip duruyor
confighelper'a
{
  'User': 'sa',
  'Password': 'secretP4ssword',
  'Hostname': 'localhost',
  'Port': '14334'
}
şeklinde eklendi


docker run -d --name mongo-server -p 27017:27017 mongo:latest
confighelper'a
{
  'HostUrl': 'mongodb://localhost:27017'                  
}
şeklinde eklendi

docker run -d --name mongo-client -p 3000:3000 mongoclient/mongoclient:latest
bununla http://localhost:3000/ adresinden önceki adımda kurduğumuz mongoya erişebiliriz. 
veya bunu kurmak yerine bilgisayara MongoDb Compass kurup önceki adımda kurduğumuz mongoya erişebiliriz. 

