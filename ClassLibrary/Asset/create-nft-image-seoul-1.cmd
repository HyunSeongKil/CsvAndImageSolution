for %%x in (����_�̺�Ʈ����, ����_������, ����_������, ����_���ϱ�, ����_������, ����_���Ǳ�, ����_������, ����_���α�, ����_��õ��, ����_�����, ����_������, ����_���빮��, ����_���۱�) do (
	echo %date% %time%
	dotnet .\NftImageCreator\bin\Debug\net6.0\NftImageCreator.dll e:\nft_img_0320\CsvToVtAndServiceable\ClassLibrary\Asset\%%x.config.json
	xcopy /q /i c:\temp\%%x e:\nft_img_0320\%%x
	start rd /s /q c:\temp\%%x
)
