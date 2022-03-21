for %%x in (서울_이벤트토지, 서울_강남구, 서울_강동구, 서울_강북구, 서울_강서구, 서울_관악구, 서울_광진구, 서울_구로구, 서울_금천구, 서울_노원구, 서울_도봉구, 서울_동대문구, 서울_동작구) do (
	echo %date% %time%
	dotnet .\NftImageCreator\bin\Debug\net6.0\NftImageCreator.dll e:\nft_img_0320\CsvToVtAndServiceable\ClassLibrary\Asset\%%x.config.json
	xcopy /q /i c:\temp\%%x e:\nft_img_0320\%%x
	start rd /s /q c:\temp\%%x
)
