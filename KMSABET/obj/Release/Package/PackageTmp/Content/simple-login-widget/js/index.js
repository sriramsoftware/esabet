
    // The "addFormError()" function, when called, adds the "error" class to the form-group that wraps around the "formRow" attribute;

    function addFormError(formRow, errorMsg) {
      var errorMSG = '<span class="error-msg">' + errorMsg + '</span>';
      $(formRow).parents('.form-group').addClass('has-error');
      $(formRow).parents('.form-group').append(errorMSG);
      $('#dialog').removeClass('dialog-effect-in');
      $('#dialog').addClass('shakeit');
      setTimeout(function() {
        $('#dialog').removeClass('shakeit');
      }, 300);
    }

    // LOGIN FORM: Validation function
    function validate_login_form() {
        var anyError = false;
        $(".form-group.has-error").removeClass("has-error")
        var form_name = "login_form1";
        var form = document.forms[form_name];
        if (form["username"].value == "") {
            // if username variable is empty
            addFormError(form["username"], 'The username is required');
            anyError = true; // stop the script if validation is triggered
        }

        if (form["password"].value == "") {
            // if password variable is empty
            addFormError(form["password"], 'The password is required');
            anyError = true; // stop the script if validation is triggered
        }

        return !anyError;
    }

    