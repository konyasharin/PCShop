import Main from './Main/Main.tsx';
import TOPBuilds from './TOPBuilds/TOPBuilds.tsx';
import LastBuilds from './LastBuilds/LastBuilds.tsx';
import { useEffect, useRef, useState } from 'react';
import getPopularBuilds from 'api/assemblies/getPopularBuilds.ts';
import getLastBuilds from 'api/assemblies/getLastBuilds.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import TAssembly from 'types/TAssembly.ts';
import useBorderValues from 'hooks/useBorderValues.ts';

function MainPage() {
  const [TOPBuildsBlocks, setTOPBuildsBlocks] = useState<TAssembly<string>[]>(
    [],
  );
  const [lastBuilds, setLastBuilds] = useState<TAssembly<string>[]>([]);
  const [offsetLastBuilds, setOffsetLastBuilds] = useBorderValues(0, 0);
  const dispatch = useDispatch();
  const isLoaded = useRef(false);
  useEffect(() => {
    dispatch(setIsLoading(true));
    getLastBuilds(offsetLastBuilds).then(response => {
      setLastBuilds(response.data.assemblies);
      dispatch(setIsLoading(false));
    });
  }, [offsetLastBuilds]);
  useEffect(() => {
    async function getMainPageBlocks() {
      dispatch(setIsLoading(true));
      setTOPBuildsBlocks((await getPopularBuilds()).data.assemblies);
      setLastBuilds((await getLastBuilds(offsetLastBuilds)).data.assemblies);
      dispatch(setIsLoading(false));
    }
    if (isLoaded.current) return;
    void getMainPageBlocks();
    isLoaded.current = true;
  }, []);
  return (
    <>
      <Main />
      <TOPBuilds TOPBuilds={TOPBuildsBlocks} />
      <LastBuilds
        lastBuilds={lastBuilds}
        offset={offsetLastBuilds}
        setOffset={setOffsetLastBuilds}
      />
    </>
  );
}

export default MainPage;
