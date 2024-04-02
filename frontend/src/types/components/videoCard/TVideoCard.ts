import TComponent from 'types/components/TComponent.ts';
import TVideoCardFilters from 'types/components/videoCard/TVideoCardFilters.ts';

type TVideoCard<T extends File | string> = TComponent<T> & TVideoCardFilters;

export default TVideoCard;
