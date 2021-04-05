docker build -t webapi -f hsear/hsear.scanner.api/Dockerfile .
docker build -t webapi -f hsear/hsear.scanner.api/Dockerfile .
docker build -t sceneexportservice -f SceneExportService/Dockerfile .
docker network create --driver bridge hsear_network
cd  HseAr
docker compose up -d