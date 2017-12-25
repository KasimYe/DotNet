# DotNet First Core WebApi
==========================

## 备忘、记录

-------------
### Dapper调用存储过程的坑
--返回值有null时泛型运行null
<pre class="brush:c#;toolbar: true; auto-links: false;">param.Get<decimal?>("@SupplierReturn")
	//报错信息
	//Attempting to cast a DBNull to a non nullable type! Note that out/return parameters will not have updated values until the data stream completes (after the 'foreach' for Query(..., buffered: false), or after the GridReader has been disposed for QueryMultiple)
</pre>

### 连接池满的问题
--一直用ado.net操作数据库基本上不会遇到这个问题，用框架第一次遇到
<pre class="brush:c#;toolbar: true; auto-links: false;">using(Connection)
	{
		……
		Connection.Close();
		Connection.Dispose();
	}</pre>
--这样的写法依然没有解决问题，最后在工厂类申明的地方判断并且关闭解决的问题 
<pre class="brush:c#;toolbar: true; auto-links: false;">if (_connection != null)
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
        _connection.Dispose();
        _connection = null;
    }</pre>
