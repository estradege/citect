# citect
Toolkit over Citect SCADA software

1) Read and Write Tags
using (var ctApi = new CtApi())
{
    ctApi.Open();
    ctApi.TagWrite("MyTagName", "MyTagValue");
    var myTag = ctApi.TagRead("MyTagName");
}
