import TComponent from 'types/components/TComponent.ts';
import TComponentFilters from 'types/components/TComponentFilters.ts';
import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';

type TVideoCard<T extends File | string> = TComponent<T> &
  TComponentFilters &
  TVideoCardFilters;

export default TVideoCard;
