cd FrontWeb/front
sudo docker build -t front  .
cd ../webar
sudo docker build -t webar  .
cd ../../Back/SceneExportService
sudo docker build -t sceneexportservice .
cd ../
sudo docker build -t webapi -f HseAr/HseAr.WebPlatform.Api/Dockerfile .
sudo docker build -t scanner -f HseAr/HseAr.Scanner.Api/Dockerfile .
cd ../
sudo docker-compose up -d