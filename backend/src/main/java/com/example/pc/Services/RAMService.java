package com.example.pc.Services;

import com.example.pc.IServices.IRAMService;
import com.example.pc.Models.RAMModel;
import com.example.pc.repositories.RAMRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class RAMService implements IRAMService {

    private final RAMRepo ramRepo;

    @Autowired
    public RAMService(RAMRepo ramRepo) {
        this.ramRepo = ramRepo;
    }

    @Override
    public List<RAMModel> getRAM() {
        return ramRepo.findAll();
    }

    @Override
    public RAMModel getRAMById(Long id) {
        return ramRepo.findById(id).orElse(null);
    }

    @Override
    public RAMModel addRAM(RAMModel ramModel) {
        return ramRepo.save(ramModel);
    }

    @Override
    public void deleteRAMById(Long id) {
        ramRepo.deleteById(id);
    }
}
