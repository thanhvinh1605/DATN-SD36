$(document).ready(function () {

    initializeProgramCategorize();


    $('input[name="RadioExchange"]').change(function () {
        var selectedValue = $('input[name="RadioExchange"]:checked').val();
        if (selectedValue === 'Study') {
            $('#addSubject').show();
        } else {
            $('#inputSubject').empty();
            $('#addSubject').hide();
        }
        if (selectedValue === 'Other') {
            $('#ProgramCategorize').empty();
            $('#showProgramCategorize').hide();
            $('#showProgramCountry').show();
        } else {
            $('#showProgramCategorize').show();
            $('#showProgramCountry').hide();
            $.ajax({
                type: "POST",
                url: "/Programs/UpdateListProgramCategorize",
                data: { code: selectedValue },
                success: function (data) {
                    if (data.success) {
                        updateProgramCategorizeSelect(data.list);
                    }
                }
            });
        }
    });

    function initializeProgramCategorize() {
        var initialCode = 'Study';
        $('#showProgramCategorize').show();
        $('#showProgramCountry').hide();
        $('#addSubject').show();
        $.ajax({
            type: "POST",
            url: "/Programs/UpdateListProgramCategorize",
            data: { code: initialCode },
            success: function (data) {
                if (data.success) {
                    updateProgramCategorizeSelect(data.list);
                }
            }
        });
    }

    function updateProgramCategorizeSelect(list) {
        var select = $('#ProgramCategorize');
        select.empty();
        select.append($('<option>', {
            value: "",
            text: "Chọn phân loại chương trình",
            disabled: true,
            selected: true
        }));

        $.each(list, function (index, item) {
            select.append($('<option>', {
                value: item.masterType,
                text: item.masterName
            }));
        });
    }

    $('#ProgramTitle').on('blur', function () {
        var title = $('#ProgramTitle').val();
        if (!title || !title.trim()) {
            $('#ProgramTitleError').text('Tên chương trình không được để trống');
            $('#ProgramTitleError').removeClass('d-none');
        } else {
            $('#ProgramTitleError').addClass('d-none');
        }
    });

    $('#PaymentValue').on('blur', function () {
        var payment = $('#PaymentValue').val();
        if (payment && payment !== '' && payment <= 0) {
            $('#PaymentValueError').text('Vui lòng nhập chi phí lớn hơn 0');
            $('#PaymentValueError').removeClass('d-none');
        } else {
            $('#PaymentValueError').addClass('d-none');
        }
    });

    $('#Participants').on('blur', function () {
        var title = $('#Participants').val();
        if (!title || !title.trim()) {
            $('#ParticipantsError').text('Đối tượng tham gia không được để trống');
            $('#ParticipantsError').removeClass('d-none');
        } else {
            $('#ParticipantsError').addClass('d-none');
        }
    });

    $('#EndDate').on('blur', function () {
        validateDateRange($('#StartDate'), $('#EndDate'), $('#EndDateError'), 'Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc');
    });

    $('#StartDate').on('blur', function () {
        validateDateRange($('#StartDate'), $('#EndDate'), $('#EndDateError'), 'Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc');
    });

    $('#Register_EndDate').on('blur', function () {
        validateDateRange($('#Register_StartDate'), $('#Register_EndDate'), $('#Register_EndDate_Error'), 'Ngày mở đơn phải nhỏ hơn hoặc bằng ngày đóng đơn');
    });

    $('#Register_StartDate').on('blur', function () {
        validateDateRange($('#Register_StartDate'), $('#Register_EndDate'), $('#Register_EndDate_Error'), 'Ngày mở đơn phải nhỏ hơn hoặc bằng ngày đóng đơn');
    });

    $('#Payment_EndDate').on('blur', function () {
        validateDateRange($('#Payment_StartDate'), $('#Payment_EndDate'), $('#Payment_EndDate_Error'), 'Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc');
    });

    $('#Payment_StartDate').on('blur', function () {
        validateDateRange($('#Payment_StartDate'), $('#Payment_EndDate'), $('#Payment_EndDate_Error'), 'Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc');
    });

    function validateDateRange(startDateId, endDateId, errorId, message) {
        var startDate = new Date(startDateId.val());
        var endDate = new Date(endDateId.val());

        if (endDate < startDate) {
            errorId.text(message);
            errorId.removeClass('d-none');
            endDateId.val('');
        } else {
            errorId.addClass('d-none');
        }
    }
});

var quill = new Quill('#editor', {
    theme: 'snow'
});
document.querySelector('form').addEventListener('submit', function (event) {
    var editorContent = document.querySelector('.ql-editor').innerHTML;
    event.preventDefault();
});

var quill = new Quill('#editor2', {
    theme: 'snow'
});
document.querySelector('form').addEventListener('submit', function (event) {
    var editorContent = document.querySelector('.ql-editor').innerHTML;
    event.preventDefault();
});

const validImageTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/svg+xml', 'image/webp'];

async function validateImgFile(file) {
    if (validImageTypes.indexOf(file.type) === -1) {
        await showAlert("Vui lòng chỉ chọn tệp hình ảnh (jpeg, png, gif, svg hoặc webp).", "warning");
        return false;
    }
    return true;
}

const fileInputImgAdd = document.getElementById('fileInputImageAdd');
const imagePreviewAdd = document.getElementById('imagePreviewAdd');
const clearImgAdd = document.getElementById('clearImgAdd');

fileInputImgAdd.addEventListener('change', function () {
    const file = fileInputImgAdd.files[0];
    if (!validateImgFile(file)) {
        // Đặt lại giá trị của input file và xóa xem trước ảnh
        fileInputImgAdd.value = '';
        clearImgPreviewAdd();
        return;
    }
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            clearImgAdd.innerHTML = '';
            const previewImg = document.createElement('img');
            previewImg.src = e.target.result;
            previewImg.className = 'img-thumbnail';
            imagePreviewAdd.innerHTML = '';
            imagePreviewAdd.appendChild(previewImg);
            const clearIcon = document.createElement('i');
            clearIcon.className = 'post-add-icon';
            clearIcon.setAttribute('data-feather', 'x');
            clearImgAdd.onclick = clearImgPreviewAdd; // Sử dụng hàm clearImgPreview để xóa ảnh
            clearImgAdd.appendChild(clearIcon);
            feather.replace(); // Render Feather icons


        };
        reader.readAsDataURL(file);
    }
});

function clearImgPreviewAdd() {
    imagePreviewAdd.innerHTML = ''; // Xóa ảnh hoặc file xem trước
    fileInputImgAdd.value = ''; // Đặt lại giá trị của input file
    clearImgAdd.innerHTML = '';
}

function getPartnerValueToList() {
    //partner country
    var partnerCountry = document.getElementById('PartnerCountry');
    var partnerCountry_Option = partnerCountry.options[partnerCountry.selectedIndex];
    var countryValue = '';
    var countryText = '';
    if (partnerCountry_Option) {
        countryValue = partnerCountry_Option.value;
        countryText = partnerCountry_Option.text;
    }

    // partner Name
    var PartnerName = document.getElementById('PartnerName');
    var PartnerName_Option = PartnerName.options[PartnerName.selectedIndex];
    var partnerValue = '';
    var partnerText = '';
    if (PartnerName_Option) {
        partnerValue = PartnerName_Option.value;
        partnerText = PartnerName_Option.text;
    }

    // partner Contact
    var PartnerContact = document.getElementById('PartnerContact');
    var PartnerContact_Option = PartnerContact.options[PartnerContact.selectedIndex];
    var contactValue = '';
    var contactText = '';
    if (PartnerContact_Option) {
        var contactValue = PartnerContact_Option.value;
        var contactText = PartnerContact_Option.text;
    }

    // max Number
    var MaxNumberRegister = document.getElementById('MaxNumberRegister').value;

    if (!countryValue || countryValue.trim() == '') {
        showAlert('Vui lòng chọn một quốc gia.', 'warning');
        return;
    }
    else if (!partnerValue || partnerValue.trim() == '') {
        showAlert('Vui lòng chọn một đối tác.', 'warning');
        return;
    }
    else if (!contactValue || contactValue.trim() == '') {
        showAlert('Vui lòng chọn một liên hệ của đối tác.', 'warning');
        return;
    }
    else if (!MaxNumberRegister || MaxNumberRegister <= 0) {
        showAlert('Vui lòng nhập số lượng tham gia lớn hơn 0.', 'warning');
        return;
    }

    const partnerIdInputs = document.querySelectorAll('input[type="hidden"][name="partnerId"]');
    let showAlertFlag = false;
    partnerIdInputs.forEach(input => {
        if (input.value.trim() === partnerValue.trim()) {
            showAlert('Đối tác đã được chọn. Vui lòng chọn đối tác khác.', 'warning');
            showAlertFlag = true;
            return;
        }
    });

    if (!showAlertFlag) {
        addPartnerToList(partnerValue, countryValue, contactValue, countryText, partnerText, MaxNumberRegister, contactText);
    }
}

function addPartnerToList(partnerID, countryCode, contactId, country, partner, number, contact) {

    const listItem = document.createElement("div");
    listItem.setAttribute("id", "partner_" + partnerID);
    listItem.innerHTML = `
                <div id="inputPartner" class="border border-gray-400 p-4 rounded-3 mb-3">
                    <div class="row gx-3" >
                        <div class="mb-2 col-md-6" >
                            <label class="small mb-1 fw-600" for= "partnerContryDisp"> Quốc gia : </label>
                            <label class="small mb-1" id="partnerContryDisp_${partnerID}" name="partnerContryDisp">${country}</label>
                        </div>
                        <div class="mb-2 col-md-6">
                            <label class="small mb-1 fw-600" for= "partnerNameDisp"> Tên đối tác : </label>
                            <label class="small mb-1" id="partnerNameDisp_${partnerID}" name="partnerNameDisp"> ${partner} </label>
                         </div>
                    </div>
                    <div class="row gx-3">
                        <div class="mb-2 col-md-6" >
                            <label class="small mb-1 fw-600" for= "maxNumberRegist" > Số lượng tham gia: </label>
                            <label class="small mb-1" id="maxNumberRegist_${partnerID}" name="maxNumberRegist"> ${number} </label>
                        </div>
                        <div class="mb-2 col-md-6">
                            <label class="small mb-1 fw-600" for= "partnerContactDisp" > Liên hệ chính : </label>
                            <label class="small mb-1" id="partnerContactDisp_${partnerID}" name="partnerContactDisp"> ${contact} </label>
                        </div>
                    </div>
                    <input type="hidden" name="partnerId" value="${partnerID}"/>
                    <input type="hidden" name="partnerCountryCode" value="${countryCode}"/>
                    <input type="hidden" name="partnerContactId" value="${contactId}"/>
                    <input type="hidden" name="partnerMaxRegist" value="${number}"/>
                    <button class="btn btn-icon border-blue text-blue me-2" type="button"> <i data-feather="edit"> </i></button>
                    <button class="btn btn-icon border-red text-red" type="button" onclick="RemovePartnerFromList('${partnerID}')"> <i data-feather="trash-2"> </i></button>
                </div>
            `;

    const listPartner = document.getElementById("listPartner");
    listPartner.appendChild(listItem);
    feather.replace();
}

function RemovePartnerFromList(IdPartner) {

    var listPartnerDiv = document.querySelector('#listPartner');
    var partnerRemove = document.querySelector('#partner_' + IdPartner);

    if (listPartnerDiv && partnerRemove) {
        listPartnerDiv.removeChild(partnerRemove);
    }
}

var countQuestion = 0;

function getQuestionValueToList() {
    var editor = document.querySelector('#editor2 .ql-editor');
    if (!editor || editor.textContent.trim() == '') {
        showAlert("Vui lòng nhập nội dung câu hỏi", "warning");
        return;
    }
    questionValue = editor.innerHTML;
    const listItem = document.createElement("div");
    listItem.setAttribute("id", "question_" + countQuestion);
    listItem.innerHTML = `
                        <div id="inputPartner" class="border border-gray-400 p-4 rounded-3 mb-5">
                            <div class="mb-3" >
                                <label class="small mb-1">${questionValue}</label>
                            </div>
                            <input type="hidden" name="questionValue" value="${questionValue}"/>
                            <button class="btn btn-icon border-blue text-blue me-2" type="button"> <i data-feather="edit"> </i></button>
                            <button class="btn btn-icon border-red text-red" type="button" onclick="RemoveQuestionFromList('${countQuestion}')"> <i data-feather="trash-2"> </i></button>
                        </div>
                    `;

    const listMoreQuestion = document.getElementById("listMoreQuestion");
    listMoreQuestion.appendChild(listItem);
    feather.replace();
    countQuestion++;
}

function RemoveQuestionFromList(questionCount) {

    var listMoreQuestion = document.querySelector('#listMoreQuestion');
    var questionRemove = document.querySelector("#question_" + questionCount);

    if (listMoreQuestion && questionRemove) {
        listMoreQuestion.removeChild(questionRemove);
    }
}