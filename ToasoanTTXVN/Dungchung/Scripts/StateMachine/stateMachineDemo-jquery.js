;(function() {
    jsPlumbDemo.initEndpoints = function(nextColour) {
        $(".ep").each(function(i, e) {
            var p = $(e).parent();
            jsPlumb.makeSource($(e), {
                parent: p,
                //anchor: [[0.2, 0, 0, -1], [1, 0.2, 1, 0], [0.8, 1, 0, 1], [0, 0.8, -1, 0]],
                anchor: "Continuous",
                connector: ["StateMachine", { curviness: 30}],
                connectorStyle: { strokeStyle: nextColour(), lineWidth: 2 },
                maxConnections: -1,
                onMaxConnections: function(info, e) {
                    alert("Maximum connections (" + info.maxConnections + ") reached");
                }
            });
        });
    };
})();