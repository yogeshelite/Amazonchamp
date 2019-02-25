$(function(){
  'use strict';

 var ctx = document.getElementById('chartStacked2').getContext('2d');
new Chart(ctx, {
type: 'bar',
data: {
  labels: ['Title', 'Features', 'Des', 'Images'],
  datasets: [{
    label: '# of Votes',
    data: [12, 39, 20, 10],
    backgroundColor: '#27AAC8'
  }]
},
options: {
  legend: {
    display: false,
      labels: {
        display: false
      }
  },
  scales: {
    yAxes: [{
      ticks: {
        beginAtZero:true,
        fontSize: 10,
        max: 100
      }
    }],
    xAxes: [{
      ticks: {
        beginAtZero:true,
        fontSize: 11
      }
    }]
  }
}
});
  
 



});
