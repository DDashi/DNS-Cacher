���� server.cs:

������������� ������������ ��� System.Net, System.Net.Sockets, System.Text, DNS_cacher � Server.
������������ ����� SendData, ������� ��������� ����� �������, ����� � IP-�����, � ���������� ������ �������.
���������� ����� Read() �� ������ FileCache.
�������� ������ IPEndPoint � ������� IPAddress.Any � ������ 53 ��� ������� �������.
�������� ������ Socket, ������� ��������� ��������� �� ipPoint � ���������� �������� TCP.
���������� ����� Bind ������, ��������� ��� ��������� ipPoint.
��������� ��������� "Server start".
����������� ����������� ���� while(true).
���������� ����� Listen ������, ������� ��������� ����� � ������� ����������� �������.
�������� ������ Socket ��� ������� � ������� ������ Accept.
��������� ������ ������ data � ���������� size ��� ��������� ������ �� �������.
���������� ������ ������������ � ������� ������ Encoding.UTF8.GetString � ����������� � ���������� dataString.
����������� ������� ���� ��� ������������� ������. ���� ��� ���� � ��� ����� ����� (TTL) �� �������, �� ������ ������������ ������� � ������� ������ SendData, � ��������� ��������� � �������.
���� ���� ��� ��� ��� TTL �������, �� ���������� ������� ��������� ������ �� DNS-������� � ������� ������ GetHostEntry. � ������ ������ ��������� ��������� �� ������ � ������� � ���������� ������� � ��������� �������� �����.
���� ������ ������� ��������, �� ����������� � ����, ������������ ������� � ��������� ��������� � �������.
����������� ���������� � ��������.
���������� ����� Save() �� ������ FileCache.

���� cache.cs:

������������ ����� Cache � ����� ����������: Ip, Domen � Ttl.
������������ ��� ������������: ������ ������� ����������� ��� �������������� � ����������� � �����������, ������� ��������� IP-�����, ����� � ����� ����� ���� (TTL).

���� file.cs:

������������� ������������ ��� Server � System.Text.Json.
������������ ����������� ����� FileCache.
������������ ��� ��������� ������ _IPFileName � _domenFileName ��� �������� ���� ������ ���� �� IP � �� ������.
������������ ��� ��������� ������� cacheByIp � cacheByDomen ��� �������� ���� �� IP � �� ������.
ReadData(string fileName) - ����������� �����, ������� ��������� ������ �� ����� � ���������� ������� ���� Dictionary<string, Cache>. ���� ���� �� ����������, �� ��������� ������ ����. ���� �����-�� ������ � ���� ��������, �� ��� ���������.
Read() - ����������� �����, ������� ������ ������ �� ������ _IPFileName � _domenFileName � ��������� �� � ��������������� ����� cacheByIp � cacheByDomen.
SaveData(string fileName, Dictionary<string, Cache> data) - ����������� �����, ������� ����������� ������� data � JSON-������ � ���������� �� � ���� fileName.
Save() - �����, ������� ��������� ��� � ����� _IPFileName � _domenFileName.