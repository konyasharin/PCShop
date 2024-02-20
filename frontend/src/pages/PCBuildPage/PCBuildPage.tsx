import Container from '../../components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';

function PCBuildPage() {
  const [scalePercents] = useState(40);
  return (
    <Container>
      <Scale percents={scalePercents} />
    </Container>
  );
}

export default PCBuildPage;
