# token-reflector
This is an Azure function that receives an empty API call and reads the authorization JWT header bearer token 
and replays it as an API output.

Selected claims are outputted

This was used to create an API that applications can use to decode ID Tokens issued to them. A better approach
is to use various OpenId and JWT libraries to do the work yourself, but this was a hack to buy me time to get
them to learn the 'right' way.
It was also a chance to learn how to use Azure Functions.. so that's a win.
