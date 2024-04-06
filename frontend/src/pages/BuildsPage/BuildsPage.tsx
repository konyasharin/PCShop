import styles from './BuildsPage.module.css';
import Container from 'components/Container/Container.tsx';
import SearchInput from 'components/inputs/SearchInput/SearchInput.tsx';
import { useEffect, useState } from 'react';
import TAssembly from 'types/TAssembly.ts';
import ChooseComponentCard from 'components/cards/ChooseComponentCard/ChooseComponentCard.tsx';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import getBuilds from 'api/assemblies/getBuilds.ts';
import config from '../../../config.ts';

function BuildsPage() {
  const [searchString, setSearchString] = useState('');
  const dispatch = useDispatch();
  const [builds, setBuilds] = useState<TAssembly<string>[]>([]);
  const [offset, setOffset] = useState(0);
  useEffect(() => {
    dispatch(setIsLoading(true));
    getBuilds(3, offset).then(response => {
      setBuilds(response.data.assemblies);
      setOffset(offset + 3);
    });
  }, []);
  const buildsBlocks = builds.map(build => {
    return (
      <ChooseComponentCard
        img={`${config.backupUrl}/${build.image}`}
        name={build.name}
        text={build.ssdId.toString()}
        price={build.price}
        url={''}
      />
    );
  });
  return (
    <Container className={styles.block}>
      <h2 className={styles.text}>Сборки</h2>
      <SearchInput
        value={searchString}
        onChange={newValue => setSearchString(newValue)}
        placeholder={'Поиск'}
        onSearch={() => console.log('Ну типо поиск')}
      />
      {...buildsBlocks}
    </Container>
  );
}

export default BuildsPage;
