function GetScheAppPOSTFetchObject(postData){
    return {
        method: 'POST',
            headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    }
}

