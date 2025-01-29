var apiErrors = $("#apiErrorList").data("value1");
var entityValidationErrorList = $("#entityErrorList").data("value2");

if (apiErrors !== "null" && apiErrors !== undefined) {
    for (var i = 0; i < apiErrors.length; i++) {
        var error = apiErrors[i];

        // Create a new div element
        var alertDiv = document.createElement("div");
        alertDiv.className = "alert alert-danger";
        alertDiv.textContent = error;

        // Append the alert div to the apiErrorList div
        document.getElementById("errorList").appendChild(alertDiv);
    }
}

if (entityValidationErrorList !== "null" && entityValidationErrorList !== undefined) {
    for (var i = 0; i < entityValidationErrorList.length; i++) {
        
        var propertyName = entityValidationErrorList[i].Item1; // Accedi alla proprietà
        var errorMessage = entityValidationErrorList[i].Item2; // Accedi al messaggio di errore

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