# ����
	- ��� : C#
	- IDE : Visual Studio Community 2022(64-bit) preview
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
	- �̹����� �ؽ�Ʈ�� ���� ���α׷�
	- A�̹����� �о� �ؽ�Ʈ�� �̹����� �� �� B�̹����� ����
	- AddressDataCreator�� ���� ������ ������ ������ �����Ͽ� �̹����� ������
	- ���� ����� ���� Task(�񵿱� ó��) ����. default:2
	- config���� ����� ClassLibrary.Asset.template.config.json ���� ����
	- 1000�� �̹��� �����ϴµ� �� 60������ �ҿ� (i5, 8GB, SDD)
	- SSD���� �ۼ��Ұ��� �ǰ���(HDD�� SSD���� 10�̻� �� ����)
	- ����
		dotnet NftImageCreatorAsync.dll args[0]
			args[0] : config ���� ��ü ���
	- ����
		dotnet NftImageCreatorAsync.dll c:\temp\config.json