package com.example.pc.Services;

import com.example.pc.Models.RAMModel;
import com.example.pc.repositories.RAMRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class RAMService {

    private final RAMRepo ramRepo;

    @Autowired
    public RAMService(RAMRepo ramRepo) {
        this.ramRepo = ramRepo;
    }

    public List<RAMModel> getRAM() {
        return ramRepo.findAll();
    }

    public RAMModel getRAMById(Long id) {
        return ramRepo.findById(id).orElse(null);
    }

    public RAMModel saveRAM(RAMModel ramModel) {
        return ramRepo.save(ramModel);
    }

    public void deleteRAMById(Long id) {
        ramRepo.deleteById(id);
    }
}
