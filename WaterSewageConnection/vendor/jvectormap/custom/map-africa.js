// Africa
$(function(){
	$('#mapAfrica').vectorMap({
		map: 'africa_mill',
		backgroundColor: 'transparent',
		scaleColors: ['#0a3d3f'],
		zoomOnScroll:false,
		zoomMin: 1,
		hoverColor: true,
		series: {
			regions: [{
				values: gdpData,
				scale: ['#05668D', '#028090', '#00A896', '#02C39A', '#F0F3BD'],
				normalizeFunction: 'polynomial'
			}]
		},
	});
});