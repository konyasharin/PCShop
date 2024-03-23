import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsError from './BuildsError/BuildsError.tsx';
import PowerOfBuild from './PowerOfBuild/PowerOfBuild.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import Input from 'components/Input/Input.tsx';
import styles from './PCBuildPage.module.css';
import Btn from 'components/btns/Btn/Btn.tsx';
import useBuild from 'hooks/useBuild.ts';

function PCBuildPage() {
  const [buildName, setBuildName] = useState('');
  const [power, setPower] = useState<number>(0);
  const { components, setComponent, toggleError, progressOfBuild, price } =
    useBuild();
  return (
    <div className={styles.body}>
      <Container>
        <h2 className={styles.build}>Ваша сборка</h2>
        <Input
          className={styles.input}
          value={buildName}
          placeholder={'Название сборки'}
          onChange={(newBuildName: string) => setBuildName(newBuildName)}
        />
        <Scale className={styles.scale} percents={progressOfBuild} />
        <BuildsError
          type={'Warning'}
          title={'Процессор'}
          description={'Выберите процессор из списка'}
        />
        <BuildsError
          type={'Error'}
          title={'Процессор'}
          description={'Выберите процессор из списка'}
        />
        <ChooseComponents components={components} setComponent={setComponent} />
        <PowerOfBuild power={power} price={`${price}$`} />
        <Btn className={styles.button}>Cоздать пк</Btn>
      </Container>
    </div>
  );
}

export default PCBuildPage;
