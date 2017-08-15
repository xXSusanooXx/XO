class Field {
    constructor(selector, cellWidth, nOfCells) {
        this.nOfCells = nOfCells;
        this.cellWidth = cellWidth;
        var field = document.querySelector(selector);
        field.style.width = this.nOfCells * cellWidth +4 + "px";
        field.style.height = this.nOfCells * cellWidth + 34 + "px";
        field.style.display = "flex";
        field.style.flexDirection = "column";
        var innerBlock = '<div class="game-bar">' +
    '<label class="game-bar__timer timer-off" id="your-timer">0</label>' +
    '<div class="game_bar__central"></div>' +
    '<label class="game-bar__timer timer-off" id="opponent-timer">0</label>' +
    '</div>';
        var innerField = document.createElement("div");
        innerField.classList.add('game-block');
        innerField.style.border = '2px solid darkblue';
        for(var y = 0; y < this.nOfCells; y++)
            for (var x = 0; x < this.nOfCells; x++) {
                var btn = document.createElement("button");
                btn.classList.add('cell');
                btn.setAttribute('x', x);
                btn.setAttribute('y', y);
                innerField.appendChild(btn);
            }
        field.appendChild(innerField);
        field.innerHTML = innerBlock + field.innerHTML;
    }

    getCell(x, y) {
        return document.querySelector('[x="' + x + '"][y="' + y + '"]');
    }

    click(x, y, isX) {
        var cell = this.getCell(x, y);
        isX ? cell.classList.add('cell_clicked_X') : cell.classList.add('cell_clicked_O');
        cell.classList.add('cell_clicked');
    }

    startTimer(first, period) {
        var selector = first ? '#your-timer' : '#opponent-timer';
        var otherSelector = !first ? '#your-timer' : '#opponent-timer';

        if (self.interval) {
            clearInterval(self.interval);
        }
        var timer = document.querySelector(selector);
        var otherTimer = document.querySelector(otherSelector);

        
        timer.classList.remove('timer-off');
        otherTimer.classList.add('timer-off');
        //timer.classList.add('timer-on');
        //otherTimer.classList.remove('timer-on');

        if (!first) document.querySelector('.game-block').classList.add('timer-off');
        else document.querySelector('.game-block').classList.remove('timer-off');


        timer.innerHTML = period;
        self.timerTime = period;
        self.interval = setInterval(function() {
            self.timerTime--;
            if (self.timerTime === 0)
                clearInterval(self.interval);
            timer.innerHTML = parseInt(timer.innerHTML) - 1;
        },
            1000);
    }
}