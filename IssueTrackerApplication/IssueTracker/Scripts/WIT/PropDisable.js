function propDisable() {
    //console.log("propDisable called");
    $(document).ready(function () {
        $('#submitButton').prop("disabled", true);
        if (!$('#mainForm').valid()) {
            //console.log("statement TRUE");
            $('#submitButton').prop("disabled", false);            
            return false;
        }
        //else {
        //    //console.log("statement FALSE");                    
        //    //$('#mainForm').submit();
        //    $('#submitButton').prop("disabled", true);
        //}
    });
};
