# ����
	- ��� : C#
	- IDE : Visual Studio Community 2022 preview
	- Author : HyunSeongKil
	- Date : 2022-03-00

# AddressDataCreator
	- Date : 2022-03-20
	- csv���� �����͸� �����Ͽ� �ּ� ������ ������ ������ ������ �����ϴ� ���α׷�	
	- csv������ ����� ClassLibrary.Asset.template.�ּҵ����ͻ�����.csv ���� ����
	- csv���� �м� �� �ñ������� ������ ������ ������ ����. ��)������.txt ������.txt ...
	- ����
		dotnet AddressDataCreator.dll args[0] args[1]
			args[0] : csv���� ��ü ���
			args[1] : ��� ������ ����� ���
	- ����
		dotnet AddressDataCreator.dll c:\temp\seoul.csv c:\temp\seoul

# NftImageCreatorAsync
	- Date : 2022-03-20
	- nft �̹����� �����ϴ� ���α׷�
	- AddressDataCreator�� ���� ������ ������ ������ �����Ͽ� �̹����� ������
	- ���� ����� ���� Task(�񵿱� ó��) ����. default:2
	- config���� ����� ClassLibrary.Asset.template.config.json ���� ����
	- ����
		dotnet NftImageCreatorAsync.dll args[0]
			args[0] : config ���� ��ü ���
	- ����
		dotnet NftImageCreatorAsync.dll c:\temp\config.json