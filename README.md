# 정보
	- 언어 : C#
	- IDE : Visual Studio Community 2022(64-bit) preview
	- Author : HyunSeongKil
	- Date : 2022-03-00

# AddressDataCreator
	- Date : 2022-03-20
	- csv에서 데이터를 추출하여 주소 데이터 생성용 데이터 파일을 생성하는 프로그램	
	- csv파일의 사양은 ClassLibrary.Asset.template.주소데이터생성용.csv 파일 참고
	- csv파일 분석 후 시군구별로 데이터 파일을 나누어 생성. 예)강동구.txt 강서구.txt ...
	- 사용법
		dotnet AddressDataCreator.dll args[0] args[1]
			args[0] : csv파일 전체 경로
			args[1] : 결과 파일이 저장될 경로
	- 예시
		dotnet AddressDataCreator.dll c:\temp\seoul.csv c:\temp\seoul

# NftImageCreatorAsync
	- Date : 2022-03-20
	- 이미지에 텍스트를 쓰는 프로그램
	- A이미지를 읽어 텍스트를 이미지에 쓴 후 B이미지로 저장
	- AddressDataCreator에 의해 생성된 데이터 파일을 참고하여 이미지를 생성함
	- 성능 향상을 위해 Task(비동기 처리) 도입. default:2
	- config파일 사양은 ClassLibrary.Asset.template.config.json 파일 참고
	- 1000개 이미지 생성하는데 약 60초정도 소요 (i5, 8GB, SDD)
	- SSD에서 작성할것을 권고함(HDD는 SSD보다 10이상 더 느림)
	- 사용법
		dotnet NftImageCreatorAsync.dll args[0]
			args[0] : config 파일 전체 경로
	- 예시
		dotnet NftImageCreatorAsync.dll c:\temp\config.json
		
# PnuAndTokenIdInserter
	- Date : 2022-03-25
	- 신한ds에서 민팅 완료한 후 제공하는 csv파일을 이용하여 land_nft_mapng에 insert하는 프로그램
	- config 파일 : ClassLibrary\Asset\pnu_tokenid.config.json 파일 참고
	- csv 파일 : ClassLibrary\Asset\template.pnu_tokenid.csv 파일 참고
	- 사용법
		dotnet PnuAndTokenIdInserter.dll args[0]
			args[0] : config 파일 전체 경로
	- 예시
		dotnet PnuAndTokenIdInserter.dll .\pnu_tokenid.config.json
