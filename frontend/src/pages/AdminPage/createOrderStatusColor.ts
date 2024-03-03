import EOrderStatuses from 'enums/EOrderStatuses.ts';

function createOrderStatusColor(status: EOrderStatuses) {
  switch (status) {
    case EOrderStatuses.accepted:
      return '#CD0000';
    case EOrderStatuses.inProcessing:
      return '#F7A400';
    case EOrderStatuses.onTheWay:
      return '#F3CE85';
    case EOrderStatuses.delivered:
      return '#42FF00';
  }
}

export default createOrderStatusColor;
