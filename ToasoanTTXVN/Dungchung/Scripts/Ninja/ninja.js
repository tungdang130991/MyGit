// huy manh - 18/04/2015
(function($){
 $.fn.columnize = function(options) {
	var defaults = {
		width: 400,
		columns : false,
		buildOnce : false,
		overflow : false,
		doneFunc : function(){},
		target : false,
		ignoreImageLoading : true,
		columnFloat : "left",
		lastNeverTallest : false,
		accuracy : false,
		manualBreaks : false,
		cssClassPrefix : ""
	};
	options = $.extend(defaults, options);

	if(typeof(options.width) == "string"){
		options.width = parseInt(options.width,10);
		if(isNaN(options.width)){
			options.width = defaults.width;
		}
	}
    return this.each(function() {
		var $inBox = options.target ? $(options.target) : $(this);
		var maxHeight = $(this).height();
		var $cache = $('<div></div>');
		var lastWidth = 0;
		var columnizing = false;
		var manualBreaks = options.manualBreaks;
		var cssClassPrefix = defaults.cssClassPrefix;
		if(typeof(options.cssClassPrefix) == "string"){
			cssClassPrefix = options.cssClassPrefix;
		}
		var adjustment = 0;
		$cache.append($(this).contents().clone(true));
		if(!options.ignoreImageLoading && !options.target){
			if(!$inBox.data("imageLoaded")){
				$inBox.data("imageLoaded", true);
				if($(this).find("img").length > 0){
					var func = function($inBox,$cache){ return function(){
							if(!$inBox.data("firstImageLoaded")){
								$inBox.data("firstImageLoaded", "true");
								$inBox.empty().append($cache.children().clone(true));
								$inBox.columnize(options);
							}
						};
					}($(this), $cache);
					$(this).find("img").one("load", func);
					$(this).find("img").one("abort", func);
					return;
				}
			}
		}
		$inBox.empty();
		columnizeIt();
		if(!options.buildOnce){
			$(window).resize(function() {
				if(!options.buildOnce){
					if($inBox.data("timeout")){
						clearTimeout($inBox.data("timeout"));
					}
					$inBox.data("timeout", setTimeout(columnizeIt, 200));
				}
			});
		}
		function prefixTheClassName(className, withDot){
			var dot = withDot ? "." : "";
			if(cssClassPrefix.length){
				return dot + cssClassPrefix + "-" + className;
			}
			return dot + className;
		}
		function columnize($putInHere, $pullOutHere, $parentColumn, targetHeight){
			while((manualBreaks || $parentColumn.height() < targetHeight) &&
				$pullOutHere[0].childNodes.length){
				var node = $pullOutHere[0].childNodes[0];
				if($(node).find(prefixTheClassName("columnbreak", true)).length){
					return;
				}
				if($(node).hasClass(prefixTheClassName("columnbreak"))){
					return;
				}
				$putInHere.append(node);
			}
			if($putInHere[0].childNodes.length === 0) return;
			var kids = $putInHere[0].childNodes;
			var lastKid = kids[kids.length-1];
			$putInHere[0].removeChild(lastKid);
			var $item = $(lastKid);
			if($item[0].nodeType == 3){
				var oText = $item[0].nodeValue;
				var counter2 = options.width / 18;
				if(options.accuracy)
				counter2 = options.accuracy;
				var columnText;
				var latestTextNode = null;
				while($parentColumn.height() < targetHeight && oText.length){
					var indexOfSpace = oText.indexOf(' ', counter2);
					if (indexOfSpace != -1) {
						columnText = oText.substring(0, oText.indexOf(' ', counter2));
					} else {
						columnText = oText;
					}
					latestTextNode = document.createTextNode(columnText);
					$putInHere.append(latestTextNode);

					if(oText.length > counter2 && indexOfSpace != -1){
						oText = oText.substring(indexOfSpace);
					}else{
						oText = "";
					}
				}
				if($parentColumn.height() >= targetHeight && latestTextNode !== null){
					$putInHere[0].removeChild(latestTextNode);
					oText = latestTextNode.nodeValue + oText;
				}
				if(oText.length){
					$item[0].nodeValue = oText;
				}else{
					return false;
				}
			}
			if($pullOutHere.contents().length){
				$pullOutHere.prepend($item);
			}else{
				$pullOutHere.append($item);
			}
			return $item[0].nodeType == 3;
		}
		function split($putInHere, $pullOutHere, $parentColumn, targetHeight){
			if($putInHere.contents(":last").find(prefixTheClassName("columnbreak", true)).length){
				return;
			}
			if($putInHere.contents(":last").hasClass(prefixTheClassName("columnbreak"))){
				return;
			}
			if($pullOutHere.contents().length){
				var $cloneMe = $pullOutHere.contents(":first");
				if($cloneMe.get(0).nodeType != 1) return;
				var $clone = $cloneMe.clone(true);
				if($cloneMe.hasClass(prefixTheClassName("columnbreak"))){
					$putInHere.append($clone);
					$cloneMe.remove();
				}else if (manualBreaks){
					$putInHere.append($clone);
					$cloneMe.remove();
				}else if($clone.get(0).nodeType == 1 && !$clone.hasClass(prefixTheClassName("dontend"))){
					$putInHere.append($clone);
					if($clone.is("img") && $parentColumn.height() < targetHeight + 20){
						$cloneMe.remove();
					}else if(!$cloneMe.hasClass(prefixTheClassName("dontsplit")) && $parentColumn.height() < targetHeight + 20){
						$cloneMe.remove();
					}else if($clone.is("img") || $cloneMe.hasClass(prefixTheClassName("dontsplit"))){
						$clone.remove();
					}else{
						$clone.empty();
						if(!columnize($clone, $cloneMe, $parentColumn, targetHeight)){
							$cloneMe.addClass(prefixTheClassName("split"));
							if($cloneMe.children().length){
								split($clone, $cloneMe, $parentColumn, targetHeight);
							}
						}else{
							$cloneMe.addClass(prefixTheClassName("split"));
						}
						if($clone.get(0).childNodes.length === 0){
							$clone.remove();
						}
					}
				}
			}
		}
		function singleColumnizeIt() {
			if ($inBox.data("columnized") && $inBox.children().length == 1) {
				return;
			}
			$inBox.data("columnized", true);
			$inBox.data("columnizing", true);

			$inBox.empty();
			$inBox.append($("<div class='"
				+ prefixTheClassName("first") + " "
				+ prefixTheClassName("last") + " "
				+ prefixTheClassName("column") + " "
				+ "' style='width:100%; float: " + options.columnFloat + ";'></div>")); //"
			$col = $inBox.children().eq($inBox.children().length-1);
			$destroyable = $cache.clone(true);
			if(options.overflow){
				targetHeight = options.overflow.height;
				columnize($col, $destroyable, $col, targetHeight);
				if(!$destroyable.contents().find(":first-child").hasClass(prefixTheClassName("dontend"))){
					split($col, $destroyable, $col, targetHeight);
				}

				while($col.contents(":last").length && checkDontEndColumn($col.contents(":last").get(0))){
					var $lastKid = $col.contents(":last");
					$lastKid.remove();
					$destroyable.prepend($lastKid);
				}
				var html = "";
				var div = document.createElement('DIV');
				while($destroyable[0].childNodes.length > 0){
					var kid = $destroyable[0].childNodes[0];
					if(kid.attributes){
						for(var i=0;i<kid.attributes.length;i++){
							if(kid.attributes[i].nodeName.indexOf("jQuery") === 0){
								kid.removeAttribute(kid.attributes[i].nodeName);
							}
						}
					}
					div.innerHTML = "";
					div.appendChild($destroyable[0].childNodes[0]);
					html += div.innerHTML;
				}
				var overflow = $(options.overflow.id)[0];
				overflow.innerHTML = html;

			}else{
				$col.append($destroyable);
			}
			$inBox.data("columnizing", false);

			if(options.overflow && options.overflow.doneFunc){
				options.overflow.doneFunc();
			}
		}
		function checkDontEndColumn(dom){
			if(dom.nodeType == 3){
				if(/^\s+$/.test(dom.nodeValue)){
						if(!dom.previousSibling) return false;
					return checkDontEndColumn(dom.previousSibling);
				}
				return false;
			}
			if(dom.nodeType != 1) return false;
			if($(dom).hasClass(prefixTheClassName("dontend"))) return true;
			if(dom.childNodes.length === 0) return false;
			return checkDontEndColumn(dom.childNodes[dom.childNodes.length-1]);
		}

		function columnizeIt() {
			adjustment = 0;
			if(lastWidth == $inBox.width()) return;
			lastWidth = $inBox.width();

			var numCols = Math.round($inBox.width() / options.width);
			var optionWidth = options.width;
			var optionHeight = options.height;
			if(options.columns) numCols = options.columns;
			if(manualBreaks){
				numCols = $cache.find(prefixTheClassName("columnbreak", true)).length + 1;
				optionWidth = false;
			}
			if(numCols <= 1){
				return singleColumnizeIt();
			}
			if($inBox.data("columnizing")) return;
			$inBox.data("columnized", true);
			$inBox.data("columnizing", true);

			$inBox.empty();
			$inBox.append($("<div style='width:" + (Math.floor(100 / numCols))+ "%; float: " + options.columnFloat + ";'></div>")); //"
			$col = $inBox.children(":last");
			$col.append($cache.clone());
			maxHeight = $col.height();
			$inBox.empty();

			var targetHeight = maxHeight / numCols;
			var firstTime = true;
			var maxLoops = 3;
			var scrollHorizontally = false;
			if(options.overflow){
				maxLoops = 1;
				targetHeight = options.overflow.height;
			}else if(optionHeight && optionWidth){
				maxLoops = 1;
				targetHeight = optionHeight;
				scrollHorizontally = true;
			}
			for(var loopCount=0;loopCount<maxLoops && loopCount<20;loopCount++){
				$inBox.empty();
				var $destroyable, className, $col, $lastKid;
				try{
					$destroyable = $cache.clone(true);
				}catch(e){
					$destroyable = $cache.clone();
				}
				$destroyable.css("visibility", "hidden");
				for (var i = 0; i < numCols; i++) {
					className = (i === 0) ? prefixTheClassName("first") : "";
					className += " " + prefixTheClassName("column");
					className = (i == numCols - 1) ? (prefixTheClassName("last") + " " + className) : className;
					$inBox.append($("<div class='" + className + "' style='width:" + (Math.floor(100 / numCols))+ "%; float: " + options.columnFloat + ";'></div>")); //"
				}
				i = 0;
				while(i < numCols - (options.overflow ? 0 : 1) || scrollHorizontally && $destroyable.contents().length){
					if($inBox.children().length <= i){
						$inBox.append($("<div class='" + className + "' style='width:" + (Math.floor(100 / numCols))+ "%; float: " + options.columnFloat + ";'></div>")); //"
					}
					$col = $inBox.children().eq(i);
					if(scrollHorizontally){
						$col.width(optionWidth + "px");
					}
					columnize($col, $destroyable, $col, targetHeight);
					split($col, $destroyable, $col, targetHeight);

					while($col.contents(":last").length && checkDontEndColumn($col.contents(":last").get(0))){
						$lastKid = $col.contents(":last");
						$lastKid.remove();
						$destroyable.prepend($lastKid);
					}
					i++;
					if($col.contents().length === 0 && $destroyable.contents().length){
						$col.append($destroyable.contents(":first"));
					}else if(i == numCols - (options.overflow ? 0 : 1) && !options.overflow){
						if($destroyable.find(prefixTheClassName("columnbreak", true)).length){
							numCols ++;
						}
					}
				}
				if(options.overflow && !scrollHorizontally){
					var IE6 = false /*@cc_on || @_jscript_version < 5.7 @*/;
					var IE7 = (document.all) && (navigator.appVersion.indexOf("MSIE 7.") != -1);
					if(IE6 || IE7){
						var html = "";
						var div = document.createElement('DIV');
						while($destroyable[0].childNodes.length > 0){
							var kid = $destroyable[0].childNodes[0];
							for(i=0;i<kid.attributes.length;i++){
								if(kid.attributes[i].nodeName.indexOf("jQuery") === 0){
									kid.removeAttribute(kid.attributes[i].nodeName);
								}
							}
							div.innerHTML = "";
							div.appendChild($destroyable[0].childNodes[0]);
							html += div.innerHTML;
						}
						var overflow = $(options.overflow.id)[0];
						overflow.innerHTML = html;
					}else{
						$(options.overflow.id).empty().append($destroyable.contents().clone(true));
					}
				}else if(!scrollHorizontally){
					$col = $inBox.children().eq($inBox.children().length-1);
					$destroyable.contents().each( function() {
						$col.append( $(this) );
					});
					var afterH = $col.height();
					var diff = afterH - targetHeight;
					var totalH = 0;
					var min = 10000000;
					var max = 0;
					var lastIsMax = false;
					var numberOfColumnsThatDontEndInAColumnBreak = 0;
					$inBox.children().each(function($inBox){ return function($item){
						var $col = $inBox.children().eq($item);
						var endsInBreak = $col.children(":last").find(prefixTheClassName("columnbreak", true)).length;
						if(!endsInBreak){
							var h = $col.height();
							lastIsMax = false;
							totalH += h;
							if(h > max) {
								max = h;
								lastIsMax = true;
							}
							if(h < min) min = h;
							numberOfColumnsThatDontEndInAColumnBreak++;
						}
					};
				}($inBox));

					var avgH = totalH / numberOfColumnsThatDontEndInAColumnBreak;
					if(totalH === 0){
						loopCount = maxLoops;
					}else if(options.lastNeverTallest && lastIsMax){
						adjustment += 30;

						targetHeight = targetHeight + 30;
						if(loopCount == maxLoops-1) maxLoops++;
					}else if(max - min > 30){
						targetHeight = avgH + 30;
					}else if(Math.abs(avgH-targetHeight) > 20){
						targetHeight = avgH;
					}else {
						loopCount = maxLoops;
					}
				}else{
					$inBox.children().each(function(i){
						$col = $inBox.children().eq(i);
						$col.width(optionWidth + "px");
						if(i === 0){
							$col.addClass(prefixTheClassName("first"));
						}else if(i==$inBox.children().length-1){
							$col.addClass(prefixTheClassName("last"));
						}else{
							$col.removeClass(prefixTheClassName("first"));
							$col.removeClass(prefixTheClassName("last"));
						}
					});
					$inBox.width($inBox.children().length * optionWidth + "px");
				}
				$inBox.append($("<br style='clear:both;'>"));
			}
			$inBox.find(prefixTheClassName("column", true)).find(":first" + prefixTheClassName("removeiffirst", true)).remove();
			$inBox.find(prefixTheClassName("column", true)).find(':last' + prefixTheClassName("removeiflast", true)).remove();
			$inBox.data("columnizing", false);

			if(options.overflow){
				options.overflow.doneFunc();
			}
			options.doneFunc();
		}
    });
 };
})(jQuery);
