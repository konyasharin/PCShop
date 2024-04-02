import TComponent from 'types/components/TComponent.ts';
import TRAMFilters from 'types/components/RAM/TRAMFilters.ts';

type TRAM<T extends File | string> = TComponent<T> & TRAMFilters;

export default TRAM;
