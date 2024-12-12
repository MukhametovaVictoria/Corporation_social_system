function like() {
    $('.like-button').click(function () {
        let btn = $(this);
        let newsIdClass = btn[0].classList.value.split(' ').find(item => item.includes('news-id'));
        if (newsIdClass) {
            let newsId = newsIdClass.split('_')[1];
            $.ajax({
                type: 'POST',
                data: { newsId: newsId },
                async: true,
                cache: false,
                dataType: 'json',
                url: '/News/Like',
                error: function (jqXHR, textStatus, errorThrown) { alert(jqXHR.responseText); },
                success: function (info) {
                    btn.toggleClass('is-active');
                    $('.likes-count_' + newsId).text(info.likesCount);
                }
            });
        }
    })
}


function back(newsId) {
    let currentDiv = $('#slides_' + newsId + ' div.block');
    if (currentDiv.length > 0) {
        let numberStr = currentDiv[0].id.split('_')[2];
        let number = parseInt(numberStr);
        if (number > 0) {
            selectImg(newsId, number - 1);
        }
    }
}

function next(newsId) {
    let currentDiv = $('#slides_' + newsId + ' div.block');
    if (currentDiv.length > 0) {
        let numberStr = currentDiv[0].id.split('_')[2];
        let number = parseInt(numberStr);
        if (number >= 0) {
            selectImg(newsId, number + 1);
        }
    }
}

function selectImg(newsId, nextNum) {
    let currentSpan = $('#dots_' + newsId + ' span.active');
    if (currentSpan.length > 0) {
        let newSpan = $('#dot_' + newsId + "_" + nextNum.toString());
        if (newSpan.length > 0) {
            currentSpan[0].classList.remove('active');
            newSpan[0].classList.add('active');
        }
    }

    let currentDiv = $('#slides_' + newsId + ' div.block');
    if (currentDiv.length > 0) {
        let newDiv = $('#img_' + newsId + "_" + nextNum.toString());
        if (newDiv.length > 0) {
            currentDiv[0].classList.remove('block');
            newDiv[0].classList.add('block');
        }
    }
}

function getNewsIds(newsIdSelector, splitChar) {
    var blocks = $(newsIdSelector);
    var newsIds = [];
    if (blocks.length > 0) {
        for (let a = 0; a < blocks.length; a++) {
            var blockId = splitChar ? blocks[a].id?.split(splitChar) : blocks[a].id;
            if (blockId.length == 2) {
                var newsId = blockId[1].trim();
                newsIds.push(newsId);
            }
        }
    }

    return newsIds;
}