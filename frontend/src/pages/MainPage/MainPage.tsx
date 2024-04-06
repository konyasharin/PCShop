import Main from './Main/Main.tsx';
import TOPBuilds from './TOPBuilds/TOPBuilds.tsx';
import LastBuilds from './LastBuilds/LastBuilds.tsx';
import { useEffect, useRef, useState } from 'react';
import getPopularBuilds from 'api/getPopularBuilds.ts';
import getLastBuilds from 'api/lastBuilds.ts';
import TBuildPreview from 'types/TBuildPreview.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import TAssembly from 'types/TAssembly.ts';

function MainPage() {
  const [TOPBuildsBlocks, setTOPBuildsBlocks] = useState<TAssembly<string>[]>(
    [],
  );
  const [lastBuilds, setLastBuilds] = useState<TBuildPreview[]>([]);
  const dispatch = useDispatch();
  const isLoaded = useRef(false);
  useEffect(() => {
    async function getMainPageBlocks() {
      dispatch(setIsLoading(true));
      setTOPBuildsBlocks((await getPopularBuilds()).data.assemblies);
      setLastBuilds(await getLastBuilds());
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
      <LastBuilds lastBuilds={lastBuilds} setLastBuilds={setLastBuilds} />
    </>
  );
}

export default MainPage;
