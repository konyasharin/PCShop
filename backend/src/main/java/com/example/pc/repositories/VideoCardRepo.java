package com.example.pc.repositories;

import com.example.pc.Models.VideoCardModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface VideoCardRepo extends JpaRepository<VideoCardModel, Long> {
}
