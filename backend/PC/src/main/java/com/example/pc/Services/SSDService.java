package com.example.pc.Services;

import com.example.pc.Models.SSDModel;
import com.example.pc.repositories.SSDRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class SSDService {

    private final SSDRepo ssdRepo;

    @Autowired
    public SSDService(SSDRepo ssdRepo) {
        this.ssdRepo = ssdRepo;
    }

    public List<SSDModel> getSSD() {
        return ssdRepo.findAll();
    }

    public SSDModel getSSDById(Long id) {
        return ssdRepo.findById(id).orElse(null);
    }

    public SSDModel saveSSD(SSDModel ssdModel) {
        return ssdRepo.save(ssdModel);
    }

    public void deleteSSDById(Long id) {
        ssdRepo.deleteById(id);
    }
}
