// This script manages url input, requests to the Words API
// and the generation of the word cloud.

// Manage the application message when no words are returned.
function setNoWordsMessage(){
    $('#message').text('Oops, no words were returned. Try another url.');
    $('#message').css('background-color', '#f2dede');
}

// Change the message after a successful result.
function setTryAnotherMessage(){
    $('#message').text('Great. Why not try another webpage url?');
    $('#message').css('background-color', '#dff0d8');
}

// Manage the message when no input is provided.
function setNoUrlMessage() {
    $('#message').text('No url was provided. Please try again');
    $('#message').css('background-color', '#f2dede');
}

$(function () {
    // Hook into Ajax events to manage the loading spinner.
    $(document).bind("ajaxSend", function () {
        $('#spinner').show();
    }).bind("ajaxStop", function () {
        $('#spinner').hide();
    }).bind("ajaxError", function () {
        $('#spinner').hide();
    });

    // Initialise the word cloud with an empty array.
    $('#word-cloud').jQCloud([], {
        width: 800,
        height: 500
    });

    // When Create Cloud button is clicked, get the value
    // in the input box and send it to the Words API.
    $('#url-submit').on('click', function () {

        // Get the value from the input box.
        var url = $('#url-input').val();

        if (!url || !url.length)
        {
            setNoUrlMessage();
        }
        else
        {
            $.ajax({
                url: '/api/v1/words?url=' + url,
            }).done(function (words) {
                // Update the word cloud with the words we've acquired.
                $('#word-cloud').jQCloud('update', words);

                // Handle the case where the array is empty.
                if (!words.length)
                {
                    setNoWordsMessage();
                    return;
                }

                setTryAnotherMessage();
                
            }).fail(function (xmlHttpRequest, statusText, errorThrown) {
                setNoWordsMessage();
                console.log(
                  'Words request error.\n\n'
                    + 'XML Http Request: ' + JSON.stringify(xmlHttpRequest)
                    + ',\nStatus Text: ' + statusText
                    + ',\nError Thrown: ' + errorThrown);
            });
        }
    });
});