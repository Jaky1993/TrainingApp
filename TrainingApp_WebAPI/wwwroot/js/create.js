if (apiErrors != "null") {
    for (var i = 0; i < apiErrors.length; i++) {
        var error = apiErrors[i];

        // Create a new div element
        var alertDiv = document.createElement("div");
        alertDiv.className = "alert alert-danger";
        alertDiv.textContent = error;

        // Append the alert div to the apiErrorList div
        document.getElementById("apiErrorList").appendChild(alertDiv);
    }
}

var entityValidationErrorListParse = JSON.parse(entityValidationErrorList);

if (entityValidationErrorListParse != "null") {
    for (var i = 0; i < entityValidationErrorListParse.length; i++) {
        
        var propertyName = entityValidationErrorListParse[i].Item1; // Accedi alla proprietà
        var errorMessage = entityValidationErrorListParse[i].Item2; // Accedi al messaggio di errore

        if (propertyName == "Name") {
            document.getElementById("errorName").innerText = errorMessage;
        }

        if (propertyName == "Age") {
            document.getElementById("errorAge").innerText = errorMessage;
        }

        // Crea un nuovo elemento div
        var alertDiv = document.createElement("div");
        alertDiv.className = "alert alert-danger";
        alertDiv.textContent = propertyName + ": " + errorMessage; // Most
    }
}