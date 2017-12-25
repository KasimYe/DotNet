# DotNet First Core WebApi
==========================

## ��������¼

-------------
### Dapper���ô洢���̵Ŀ�
--����ֵ��nullʱ��������null
<pre class="brush:c#;toolbar: true; auto-links: false;">param.Get<decimal?>("@SupplierReturn")
	//������Ϣ
	//Attempting to cast a DBNull to a non nullable type! Note that out/return parameters will not have updated values until the data stream completes (after the 'foreach' for Query(..., buffered: false), or after the GridReader has been disposed for QueryMultiple)
</pre>

### ���ӳ���������
--һֱ��ado.net�������ݿ�����ϲ�������������⣬�ÿ�ܵ�һ������
<pre class="brush:c#;toolbar: true; auto-links: false;">using(Connection)
	{
		����
		Connection.Close();
		Connection.Dispose();
	}</pre>
--������д����Ȼû�н�����⣬����ڹ����������ĵط��жϲ��ҹرս�������� 
<pre class="brush:c#;toolbar: true; auto-links: false;">if (_connection != null)
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
        _connection.Dispose();
        _connection = null;
    }</pre>
