package com.example.pc.Services;

import com.example.pc.IServices.IPowerUnitService;
import com.example.pc.Models.PowerUnitModel;
import com.example.pc.repositories.PowerUnitRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class PowerUnitService implements IPowerUnitService {

    private final PowerUnitRepo powerUnitRepo;

    @Autowired
    public PowerUnitService(PowerUnitRepo powerUnitRepo) {
        this.powerUnitRepo = powerUnitRepo;
    }

    @Override
    public List<PowerUnitModel> getPowerUnits() {
        return powerUnitRepo.findAll();
    }

    @Override
    public PowerUnitModel getPowerUnitById(Long id) {
        return powerUnitRepo.findById(id).orElse(null);
    }

    @Override
    public PowerUnitModel addPowerUnit(PowerUnitModel powerUnitModel) {
        return powerUnitRepo.save(powerUnitModel);
    }

    @Override
    public void deletePowerUnitById(Long id) {
        powerUnitRepo.deleteById(id);
    }
}
