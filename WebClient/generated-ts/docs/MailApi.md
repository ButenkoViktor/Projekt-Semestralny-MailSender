# MailApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**mailSendPost**](MailApi.md#mailsendpost) | **POST** /mail/send |  |



## mailSendPost

> mailSendPost(sendMailRequestDto)



### Example

```ts
import {
  Configuration,
  MailApi,
} from '';
import type { MailSendPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const config = new Configuration({ 
    // Configure HTTP bearer authorization: Bearer
    accessToken: "YOUR BEARER TOKEN",
  });
  const api = new MailApi(config);

  const body = {
    // SendMailRequestDto (optional)
    sendMailRequestDto: ...,
  } satisfies MailSendPostRequest;

  try {
    const data = await api.mailSendPost(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **sendMailRequestDto** | [SendMailRequestDto](SendMailRequestDto.md) |  | [Optional] |

### Return type

`void` (Empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

