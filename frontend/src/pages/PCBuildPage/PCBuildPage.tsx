import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsError from './BuildsError/BuildsError.tsx';
import PowerOfBuild from './PowerOfBuild/PowerOfBuild.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import Input from 'components/inputs/Input/Input.tsx';
import styles from './PCBuildPage.module.css';
import Btn from 'components/btns/Btn/Btn.tsx';
import useBuild from 'hooks/useBuild.ts';
import componentTypes from 'enums/componentTypes.ts';

function PCBuildPage() {
  const [buildName, setBuildName] = useState('');
  const {
    components,
    setComponent,
    toggleError,
    progressOfBuild,
    price,
    power,
    errors,
    setIsActive,
    createBuild,
  } = useBuild();
  const errorsBlocks = errors.map(error => {
    return (
      <BuildsError
        type={error.errorType}
        title={componentTypes[error.componentType]}
        description={error.errorDescription}
      />
    );
  });
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
        {...errorsBlocks}
        <ChooseComponents
          components={components}
          setComponent={setComponent}
          setIsActive={setIsActive}
          key={0}
        />
        <PowerOfBuild power={power} price={`${price}$`} />
        <Btn className={styles.button} onClick={() => createBuild()}>
          Cоздать пк
        </Btn>
      </Container>
    </div>
  );
}

export default PCBuildPage;
