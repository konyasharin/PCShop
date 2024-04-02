import TComponent from 'types/components/TComponent.ts';
import TSSDFilters from 'types/components/SSD/TSSDFilters.ts';

type TSSD<T extends File | string> = TComponent<T> & TSSDFilters;

export default TSSD;
