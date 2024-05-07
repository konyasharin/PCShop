import orderStatuses from 'enums/orderStatuses.ts';

function createOrderStatusColor(status: keyof typeof orderStatuses) {
  switch (status) {
    case 'accepted':
      return '#CD0000';
    case 'inProcessing':
      return '#F7A400';
    case 'onTheWay':
      return '#F3CE85';
    case 'delivered':
      return '#42FF00';
  }
}

export default createOrderStatusColor;
