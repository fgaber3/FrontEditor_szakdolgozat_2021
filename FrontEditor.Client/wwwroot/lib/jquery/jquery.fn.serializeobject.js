jQuery.fn.serializeObject = function(){
    var results = {},
        arr = this.serializeArray();
    for (var i = 0, len = arr.length; i < len; i++) {
    obj = arr[i];
    //Check if results have a property with given name
    if (results.hasOwnProperty(obj.name)) {
        //Check if given object is an array
        if (!results[obj.name].push) {
        results[obj.name] = [results[obj.name]];
        }
        results[obj.name].push(obj.value || '');
    } else {
        results[obj.name] = obj.value || '';
    }
    }
    return results;
}