for %%x in (����_������, ����_���빮��, ����_���ʱ�, ����_������, ����_���ϱ�, ����_���ı�, ����_��õ��, ����_��������, ����_��걸, ����_����, ����_���α�, ����_�߱�, ����_�߶���) do (
	echo %date% %time%
	dotnet .\NftImageCreator\bin\Debug\net6.0\NftImageCreator.dll e:\nft_img_0320\CsvToVtAndServiceable\ClassLibrary\Asset\%%x.config.json
	xcopy /q /i c:\temp\%%x e:\nft_img_0320\%%x
	start rd /s /q c:\temp\%%x
)
