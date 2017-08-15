"use strict";

class Game {
    constructor() {
        //initializing
        this.client = $.connection.xOhub.client;
        this.server = $.connection.xOhub.server;
        //function to start signalr binding
        this._RegisterClientEventHandlers();
        var self = this;
        $.connection.hub.start().done(function () {
            self.server.connect();
        });
    }

    _Start(isFirstPlayer, turnTime) {
        this.isFirstPlayer = isFirstPlayer;
        this.isMyTurn = this.isFirstPlayer;
        this.turnTime = turnTime;
        this.field = new Field('.game-block-wrap', 25, 15);
        
        this._RegisterFieldEventHandlers(true);
        $(".waiter").hide();
        $(".game-block-wrap").show();
    }

    _RegisterClientEventHandlers() {
        var self = this;

        this.client.startGame = (isFirstPlayer, turnTime) =>
            this._Start(isFirstPlayer, turnTime);

        this.client.opponentMadeTurn = function(x,y) {
            self.field.click(x, y,!self.isFirstPlayer);
            self.isMyTurn = true;
            self.field.startTimer(true, self.turnTime);
        };
        this.client.win = function() {
            alert("You Win!");
        };
        this.client.lose = function() {
            alert("You Lose!");
        }
    }

    _RegisterFieldEventHandlers() {
        for(var y = 0; y < this.field.nOfCells;y++)
            for (var x = 0; x < this.field.nOfCells; x++) {
                var cell = this.field.getCell(x, y);
                var self = this;

                cell.addEventListener("click",
                    function() {
                        if (!self.isMyTurn) return;
                        if (this.classList.contains('cell_clicked')) return;

                        self.field.click(this.getAttribute('x'), this.getAttribute('y'),self.isFirstPlayer);
                        self.field.startTimer(false, self.turnTime);
                        self.server.makeTurn(this.getAttribute('x'),this.getAttribute('y'));
                        self.isMyTurn = false;
                        
                    });
            }
    }
}
    
