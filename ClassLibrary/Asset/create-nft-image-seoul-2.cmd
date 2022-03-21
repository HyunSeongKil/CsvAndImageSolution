for %%x in (서울_마포구, 서울_서대문구, 서울_서초구, 서울_성동구, 서울_성북구, 서울_송파구, 서울_양천구, 서울_영등포구, 서울_용산구, 서울_은평구, 서울_종로구, 서울_중구, 서울_중랑구) do (
	echo %date% %time%
	dotnet .\NftImageCreator\bin\Debug\net6.0\NftImageCreator.dll e:\nft_img_0320\CsvToVtAndServiceable\ClassLibrary\Asset\%%x.config.json
	xcopy /q /i c:\temp\%%x e:\nft_img_0320\%%x
	start rd /s /q c:\temp\%%x
)
